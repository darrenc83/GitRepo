CREATE PROCEDURE [dbo].[GetReportHopperMinimumAndRefill]
	-- Add the parameters for the stored procedure here
	@start DATETIME2,
	@end DATETIME2
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT hl.MachineId MachineId, hl.[Start] [Date], hl.Minimum, c.[Total] RefillTotal
		FROM
			(SELECT subtotal.Id MachineId, subtotal.[Start], MIN(subtotal.Total) Minimum FROM 
				(SELECT mr.Id, mr.[Start], mr.[End], mle.Id MachineLevelEventId,
					(SELECT SUM(ml.[Level] * ml.[Value]) Total FROM MachineLevel ml WHERE mle.Id = ml.MachineLevelEventId AND ml.[Type] = 'Coin' AND (ml.LevelType = 'CashBox' OR ml.LevelType = 'Stored')) Total
				FROM
					(SELECT m.Id, cal.[Date] [Start], DATEADD(DAY, 1, cal.[Date]) [End] 
					FROM Machine m, dbo.GenerateCalendar(@start, DATEDIFF(day, @start, @end)) cal) mr
					LEFT JOIN MachineLevelEvent mle ON (mr.Id = mle.MachineId AND mle.[Timestamp] >= mr.[Start] AND mle.[Timestamp] < mr.[End])) subtotal
			GROUP BY subtotal.Id, subtotal.[Start]) hl
		LEFT JOIN
			(SELECT subtotal.Id MachineId, subtotal.[Start], SUM(subtotal.Total) Total, COUNT(subtotal.CollectionEventId) EventCount, COUNT(subtotal.CollectionType) CollectionCount
			FROM
				(SELECT mr.Id, mr.[Start], col.CollectionEventId, col.[Level] * col.[Value] Total, ce.CollectionType
				FROM
					(SELECT m.Id, cal.[Date] [Start], DATEADD(DAY, 1, cal.[Date]) [End] 
							FROM Machine m, dbo.GenerateCalendar(@start, DATEDIFF(day, @start, @end)) cal) mr
					LEFT JOIN CollectionEvent ce ON (mr.Id = ce.MachineId AND ce.[Timestamp] >= mr.[Start] AND ce.[Timestamp] < mr.[End] AND ce.CollectionType = 'CollectorRefill')
					LEFT JOIN [Collection] col ON (col.CollectionEventId = ce.Id AND col.CurrencyType = 'Coin')
				) subtotal
			GROUP BY subtotal.Id, subtotal.[Start]) c ON (hl.MachineId = c.MachineId AND c.[Start] = hl.[Start])
		ORDER BY hl.MachineId, hl.[Start]
END