CREATE PROCEDURE [dbo].[GetSystemEventsForMachine]
    
    -- ID of the machine to get events for
    @machineId BIGINT,
    
    -- ID of the current user
    @userId NVARCHAR(128),
    
    -- (Maximum) number of events to return
    @countToReturn INT,
    
    -- If not 0, only return events before this ID (used for paging in the UI)
    @beforeId BIGINT = 0,
    
    -- If not null, only return events from this date/time
    @fromDateTime DATETIMEOFFSET = NULL,
    
    -- If not null, only return events to this date/time 
    @toDateTime DATETIMEOFFSET = NULL

AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Get the date/time the user last viewed events
    DECLARE @lastViewedDate DATETIMEOFFSET
    SELECT @lastViewedDate = LastViewedEventsTime FROM AspNetUsers WHERE Id = @userId

    SELECT TOP (@countToReturn)
        SE.Id,
        SE.Timestamp,
        SE.EventType,
        SE.MachineStatus,
        SE.MachineId,
        SE.MachineName,
        SE.SiteId,
        SE.SiteName,
        SE.CollectorId,
        SE.UserId,
        SE.UserName,
        SE.CollectionEventId,
        SE.CollectionTotalValue,
        CASE
            WHEN SE.Timestamp > @lastViewedDate THEN 1 ELSE 0
        END AS 'IsNew'
    FROM SystemEvent SE
    WHERE SE.MachineId = @machineId
    AND (@fromDateTime IS NULL OR SE.Timestamp > @fromDateTime)
    AND (@toDateTime IS NULL OR SE.Timestamp < @toDateTime)
    AND (@beforeId = 0 OR SE.Id < @beforeId)
    ORDER BY Timestamp DESC

END