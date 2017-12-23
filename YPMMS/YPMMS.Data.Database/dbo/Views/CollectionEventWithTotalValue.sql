
-- And finally, build a new view with collection event and total value information
CREATE VIEW [dbo].[CollectionEventWithTotalValue] AS
SELECT
	CE.[Id],
	CE.[CollectorId],
	CE.[MachineId],
	CE.[CollectionType],
	C.[Currency],
	SUM(C.[Value] * C.[Level]) as 'TotalValue',
	CE.[Timestamp]
FROM
	[CollectionEvent] CE
JOIN
	[Collection] C
ON
	CE.Id = C.CollectionEventId
GROUP BY
	CE.[Id],
	CE.[CollectorId],
	CE.[MachineId],
	CE.[CollectionType],
	C.[Currency],
	CE.[Timestamp]