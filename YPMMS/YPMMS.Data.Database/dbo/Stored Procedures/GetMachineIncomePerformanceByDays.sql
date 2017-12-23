-- =============================================
-- Author:		Simon Martin
-- Create date: 19 October 2016
-- Description:	Get the income performance of a machine
-- =============================================
CREATE PROCEDURE [dbo].[GetMachineIncomePerformanceByDays] 
	 @MachineId bigint
	,@Year int
	,@Month int
AS
BEGIN
	SET NOCOUNT ON;

	IF @machineId = 0 -- zero means all machines
	BEGIN
		EXEC [dbo].[AllAccumByDAY] @Year, @Month 
	END
	ELSE
	BEGIN
		SELECT *
		FROM (
			SELECT
				cal.[Day], IIF(SUM(TotalValue) IS NULL, 0, SUM(TotalValue)) Total
			FROM
				(SELECT gc.[Day] [Day], gc.[Date] [Start], DATEADD(day, 1, gc.[Date]) [End]     
				 FROM GenerateCalendar(DATEFROMPARTS(@year, @month, 1), DATEDIFF(day, DATEFROMPARTS(@year, @month, 1), DATEADD(month, 1, DATEFROMPARTS(@year, @month, 1)))) gc
				 WHERE gc.[Month] = @month) cal
			LEFT JOIN
				[CollectionEventWithTotalValue] ctv ON (ctv.[Timestamp] >= cal.[Start]
														AND ctv.[Timestamp] < cal.[End]
														AND ctv.CollectionType = 'Collection'
														AND ctv.MachineId = @MachineId)
			GROUP BY cal.[Day]
		) AS CollectionTotals
		LEFT JOIN 
		(
			select * from 
				(
				select top 1 '1' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 1 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '2' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 2 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '3' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 3 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '4' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 4 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '5' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 5 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '6' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 6 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '7' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 7 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '8' as 'day',  profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 8 and machineId = @MachineId) order by timestamp desc
				union all
				select top 1 '9' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 9 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '10' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 10 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '11' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 11 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '12' as 'day',  profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 12 and machineId = @MachineId) order by timestamp desc
				union all
				select top 1 '13' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 13 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '14' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 14 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '15' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 15 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '16' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 16 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '17' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 17 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '18' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 18 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '19' as 'day',  profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 19 and machineId = @MachineId) order by timestamp desc
				union all
				select top 1 '20' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 20 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '12' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 21 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '22' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 22 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '23' as 'day',  profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 23 and machineId = @MachineId) order by timestamp desc
				union all
				select top 1 '24' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 24 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '25' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 25 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '26' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 26 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '27' as 'day',  profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 27 and machineId = @MachineId) order by timestamp desc
				union all
				select top 1 '28' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 28 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '29' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 29 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '30' as 'day', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 30 and machineId = @MachineId) order by timestamp desc 
				union all
				select top 1 '31' as 'day',  profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nDay= 31 and machineId = @MachineId) order by timestamp desc
 				) as tt
		) AS PL on CollectionTotals.[Day] = PL.[day]
	END
END