-- =============================================
-- Author:		Simon Martin
-- Create date: 12 October 2016
-- Description:	Create a record in the MachineLevelEvent table and return its identity
-- =============================================
CREATE PROCEDURE [dbo].[CreateMachineLevelEvent]
	@MachineId bigint,
	@Timestamp datetime2(7)
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO MachineLevelEvent (MachineID,Timestamp)
	VALUES (@MachineID,@Timestamp);
	SELECT CAST(SCOPE_IDENTITY() as bigint)
END