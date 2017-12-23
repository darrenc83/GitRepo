CREATE TABLE [dbo].[SystemEvent] (
    [Id]                   BIGINT             IDENTITY (1, 1) NOT NULL,
    [Timestamp]            DATETIMEOFFSET (7) CONSTRAINT [DF_SystemEvent_Timestamp] DEFAULT (getutcdate()) NOT NULL,
    [EventType]            VARCHAR (50)       NOT NULL,
    [MachineStatus]        VARCHAR (50)       NULL,
    [MachineId]            BIGINT             NULL,
    [MachineName]          NVARCHAR (250)     NULL,
    [SiteId]               BIGINT             NULL,
    [SiteName]             NVARCHAR (100)     NULL,
    [CollectorId]          BIGINT             NULL,
    [UserId]               NVARCHAR (128)     NULL,
    [UserName]             NVARCHAR (100)     NULL,
    [CollectionEventId]    BIGINT             NULL,
    [CollectionTotalValue] MONEY              NULL,
    CONSTRAINT [PK__SystemEvent] PRIMARY KEY CLUSTERED ([Id] ASC)
);

