CREATE PROCEDURE [dbo].[GetSystemEventsForUser]
    
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

    -- Get the user's role
    DECLARE @role NVARCHAR(256)
    SELECT @role =
        R.Name
    FROM AspNetUserRoles UR
    JOIN AspNetRoles R ON UR.RoleId = R.Id
        WHERE UR.UserId = @userId

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

    -- Identify the collector
    LEFT JOIN Collector C1 ON SE.CollectorId = C1.Id

    -- Identify collector via machine
    LEFT JOIN CollectorToMachine CTM ON SE.MachineId = CTM.MachineId
    LEFT JOIN Collector C2 On CTM.CollectorId = C2.Id

    -- Identify manager via site
    LEFT JOIN ManagerToSite MTS ON SE.SiteId = MTS.SiteId
    LEFT JOIN Manager M ON MTS.ManagerId = M.Id

    WHERE
    (
        -- Always return records where the user ID is a direct match
        (SE.UserId = @userId)
        OR
        -- Admin users see everything
        (@role = 'Admin')
        OR
        (@role = 'Collector' AND (
        C1.UserId = @userId OR
        C2.UserId = @userId))
        OR
        (@role = 'Manager' AND M.UserId = @userId)
    )
    AND (@fromDateTime IS NULL OR SE.Timestamp > @fromDateTime)
    AND (@toDateTime IS NULL OR SE.Timestamp < @toDateTime)
    AND (@beforeId = 0 OR SE.Id < @beforeId)
  
    ORDER BY Timestamp DESC

END