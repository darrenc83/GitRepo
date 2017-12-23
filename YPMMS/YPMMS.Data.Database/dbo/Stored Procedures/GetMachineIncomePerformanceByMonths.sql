-- =============================================
-- Author:		Simon Martin
-- Create date: 19 October 2016
-- Description:	Get the income performance of a machine
-- =============================================
CREATE PROCEDURE [dbo].[GetMachineIncomePerformanceByMonths] 
	 @MachineId bigint
	,@Year int
AS
BEGIN
	SET NOCOUNT ON

	IF @machineId = 0 -- zero means all machines
	BEGIN
		EXEC [dbo].[AllAccumByMONTH] @Year
	END
	ELSE
	BEGIN
		SELECT *
		FROM (
			SELECT
				months.[Month], IIF(SUM(ctv.[TotalValue]) IS NULL, 0, SUM(ctv.[TotalValue])) [Total]
			FROM
				(SELECT
					gc.[Month], gc.[Date] [Start], DATEADD(day, 1, MAX(gc.LDtOfMo)) [End]
					FROM
					GenerateCalendar(DATEFROMPARTS(@year, 1, 1), DATEDIFF(day, DATEFROMPARTS(@year, 1, 1), DATEFROMPARTS(@year + 1, 1, 1))) gc
					WHERE
					gc.[Day] = 1 GROUP BY gc.[Month], gc.[Date]) months 
				LEFT JOIN
				[CollectionEventWithTotalValue] ctv ON (ctv.[Timestamp] >= months.[Start] AND
														ctv.[Timestamp] < months.[End] AND
														ctv.CollectionType = 'Collection' AND
														ctv.MachineId = @machineId)
			GROUP BY
				months.[Month]
		) AS CollectionTotals
		LEFT JOIN 
		  (
				select * from 
				  (
					select top 1 '1' as 'month', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nMonth= 1 and machineId = @MachineId) order by timestamp desc 
					union all
					select top 1 '2' as 'month', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nMonth= 2 and machineId = @MachineId) order by timestamp desc 
					union all
					select top 1 '3' as 'month', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nMonth= 3 and machineId = @MachineId) order by timestamp desc 
					union all
					select top 1 '4' as 'month', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nMonth= 4 and machineId = @MachineId) order by timestamp desc 
					union all
					select top 1 '5' as 'month', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nMonth= 5 and machineId = @MachineId) order by timestamp desc 
					union all
					select top 1 '6' as 'month', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nMonth= 6 and machineId = @MachineId) order by timestamp desc 
					union all
					select top 1 '7' as 'month', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nMonth= 7 and machineId = @MachineId) order by timestamp desc 
					union all
					select top 1 '8' as 'month',  profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nMonth= 8 and machineId = @MachineId) order by timestamp desc
					union all
					select top 1 '9' as 'month', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nMonth= 9 and machineId = @MachineId) order by timestamp desc 
					union all
					select top 1 '10' as 'month', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nMonth= 10 and machineId = @MachineId) order by timestamp desc 
					union all
					select top 1 '11' as 'month', profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nMonth= 11 and machineId = @MachineId) order by timestamp desc 
					union all
					select top 1 '12' as 'month',  profitloss, Accum from MachineLevelIncome where ( nYear=@Year and  nMonth= 12 and machineId = @MachineId) order by timestamp desc 
 				   ) as tt
		)  AS PL on CollectionTotals.[Month] = PL.[month]
	END
END