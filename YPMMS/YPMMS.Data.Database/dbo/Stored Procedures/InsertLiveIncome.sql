CREATE PROCEDURE [dbo].[InsertLiveIncome]
 	@Id bigint,
	@MachineID bigint,
	@Timestamp DateTime2,
	@Stored money,
	@Cashbox money,
	@Float money,
	@IsNewMachine bit
AS
BEGIN

	-- Local variables
	--DECLARE @currentstored int
	--DECLARE @currentcashbox int
	DECLARE @lastStored money
	DECLARE @lastProfitLoss money
	DECLARE @lastAccum money  
	DECLARE @profitLoss money
	DECLARE @Accum money
	DECLARE @iyear int
	DECLARE @imonth int
	DECLARE @iday int
	DECLARE @ihour int

	IF @IsNewMachine = 1
		BEGIN
			SET @Stored  = 0.00
			SET @Cashbox = 0.00
			SET @profitLoss =  0.00
			SET @Accum   = 0.00
			SET @Float   = 0.00
			SET @Timestamp = (SELECT TOP 1 [Timestamp] FROM Machine ORDER BY [Timestamp] DESC)
		END
	ELSE
	BEGIN
		-- 106582 or 5678, 5679 5680 ect....
		set @Stored = (
				 select sum(ML.Level * ML.Value) from [dbo].[MachineLevelEvent] MLE
				 INNER JOIN [dbo].[MachineLevel] ML ON ML.MachineLevelEventId = MLE.id
				 where MLE.id = @id
		 )
		 -- 0
		 -- 123345.66
		 -- null
		set @lastAccum = (SELECT TOP 1 [Accum] From ( select Top 2 [id], [Accum] from [dbo].[MachineLevelIncome] where MachineId = @MachineID ORDER BY id DESC)  as x  ) 
			
		IF @lastAccum IS NULL 
			BEGIN
			   SET @lastAccum = -1
		   END
			else
			BEGIN
			   set @lastAccum = @lastAccum 
			END

		set @float = ( select sum(FloatLevel * value) from [dbo].[MachineDenomination] where MachineId = @MachineID )
	END

	set @iyear  = YEAR(@timestamp)
	set @imonth = MONTH(@timestamp)
	set @iday   = DAY(@timestamp)
	set @ihour  = cast(DATEPART(hour, @timestamp) as varchar)

	IF @lastAccum >-1   
	BEGIN
	   SET @lastAccum = -1
	   set @profitLoss =  @Stored - @float
	   set @Accum = @profitLoss + @lastAccum
	END

	INSERT INTO  [dbo].[MachineLevelIncome] (id, Machineid, [Timestamp], Stored, CashBox, Profitloss, Accum, nYear, nMonth, nDay, nHour,[float] )
	Values (
		@Id,
		@MachineId,
		@Timestamp,
		@Stored,
		@Cashbox,
		@ProfitLoss,
		@Accum,
		@iyear,
		@imonth,
		@iday,
		@ihour,
		@Float
	)
END