-- =============================================
-- Author:		Simon Martin
-- Create date: 12 October 2016
-- Description:	Create a record in the MachineLevel table
-- =============================================
CREATE PROCEDURE [dbo].[CreateMachineLevel] 
	@Type varchar(10),
	@LevelType varchar(30),
	@Currency varchar(6),
	@Level int,
	@Value money,
	@MachineLevelEventId bigint
AS
BEGIN
	INSERT INTO MachineLevel (Type,LevelType,Currency,	Level,Value,MachineLevelEventId)
	VALUES (@Type,@LevelType,@Currency,	@Level,@Value,@MachineLevelEventId)
END