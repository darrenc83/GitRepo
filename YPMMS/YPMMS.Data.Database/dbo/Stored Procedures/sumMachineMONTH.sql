
CREATE PROCEDURE [dbo].[sumMachineMONTH]
  @year int,
  @month int,
  @Acc money OUTPUT
AS
BEGIN
 	SET NOCOUNT ON;
DECLARE @machineid bigint,  @value money = 0, @temp money = 0
DECLARE db_cursor CURSOR FOR select id from [dbo].[Machine] 
OPEN db_cursor   
FETCH NEXT FROM db_cursor INTO @machineid  
	WHILE @@FETCH_STATUS = 0   
	BEGIN   
	-- get the last occurance of the month for each machine
	-- change  this [dbo].[MachineValues] to  the name of the table i think its called LiveMachineIncome
      set @temp = ( select top 1 Accum from [dbo].[MachineLevelIncome]  where (machineid=@machineid and [nYear]=@year and [nMonth]=@month) order by [Timestamp] desc  ) 
       if ( len(@temp) >0  )
		  begin
			set @value += @temp
	 	  end 
 	 	   FETCH NEXT FROM db_cursor INTO @machineid  
	END   
	SELECT @Acc =  @value
 CLOSE db_cursor   
DEALLOCATE db_cursor
 
END