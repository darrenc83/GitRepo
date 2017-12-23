CREATE PROCEDURE CreateSystemEvent
    @EventType          VARCHAR(50),
    @MachineStatus      VARCHAR(50)     = NULL,
    @MachineId          BIGINT          = NULL,
    @MachineName        NVARCHAR(250)   = NULL,
    @SiteId             BIGINT          = NULL,
    @SiteName           NVARCHAR(100)   = NULL,
    @CollectorId        BIGINT          = NULL,
    @UserId             NVARCHAR(128)   = NULL,
    @CollectionEventId  BIGINT          = NULL
AS
BEGIN
    -- This sproc is provided to make system events easier to create.
    -- Instead of having to pass all the system event properties in at creation time
    -- (some of which the calling code may not even know), you can pass just the
    -- properties you do know, and the sproc will fill in the rest.
    --
    -- For example, to create a system event for a collection event you just need to
    -- do this:
    --
    --    EXEC CreateSystemEvent
    --          @EventType = 'MachineCollected',
    --          @CollectionEventId = x
    --
    -- The sproc will fill in MachineId, MachineName, SiteId, SiteName, CollectorId, UserId
    -- and CollectionTotalValue for you.
    
    -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert the parameters into a temporary table variable so we can join it against other tables
    DECLARE @tempTable TABLE(
        EventType           VARCHAR(50),
        MachineStatus       VARCHAR(50),
        MachineId           BIGINT,
        MachineName         NVARCHAR(250),
        SiteId              BIGINT,
        SiteName            NVARCHAR(100),
        CollectorId         BIGINT,
        UserId              NVARCHAR(128),
        CollectionEventId   BIGINT)

    INSERT @tempTable (EventType, MachineStatus, MachineId, MachineName, SiteId, SiteName, CollectorId, UserId, CollectionEventId)
    VALUES (@EventType, @MachineStatus, @MachineId, @MachineName, @SiteId, @SiteName, @CollectorId, @UserId, @CollectionEventId)


    INSERT SystemEvent(
        EventType,
        MachineStatus,
        MachineId,
        MachineName,
        SiteId,
        SiteName,
        CollectorId,
        UserId,
        UserName,
        CollectionEventId,
        CollectionTotalValue
    )
    SELECT
        TT.EventType,
        TT.MachineStatus,
        COALESCE(TT.MachineId, M2.Id),
        COALESCE(TT.MachineName, M1.Name, M2.Name),
        COALESCE(TT.SiteId, M1.SiteId),
        COALESCE(TT.SiteName, S1.Name, S2.Name),
        COALESCE(TT.CollectorId, C1.Id, CE1.CollectorId),
        COALESCE(TT.UserId, U2.Id, U3.Id),
        COALESCE(U1.Name, U2.Name, U3.Name),
        TT.CollectionEventId,
        CE1.TotalValue
    FROM @tempTable TT
    LEFT JOIN Machine M1 ON TT.MachineId = M1.Id
    LEFT JOIN Site S1 ON TT.SiteId = S1.Id
    -- Identify the site via the machine (if @SiteId isn't specified)
    LEFT JOIN Site S2 ON M1.SiteId = S2.Id
    LEFT JOIN CollectionEventWithTotalValue CE1 ON TT.CollectionEventId = CE1.Id
    -- Identify the machine via the collection event
    LEFT JOIN Machine M2 ON CE1.MachineId = M2.Id
    LEFT JOIN Collector C1 ON TT.CollectorId = C1.Id
    -- Identify the collector via the collection event
    LEFT JOIN Collector C2 ON CE1.CollectorId = C2.Id
    LEFT JOIN AspNetUsers U1 ON TT.UserId = U1.Id
    -- Identify the user via the collector
    LEFT JOIN AspNetUsers U2 ON C1.UserId = U2.Id
    -- Identify the user via the collection event collector
    LEFT JOIN AspNetUsers U3 ON C2.UserId = U3.Id

END