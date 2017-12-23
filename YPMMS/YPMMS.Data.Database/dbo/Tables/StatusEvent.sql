CREATE TABLE [dbo].[StatusEvent] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [MachineId] BIGINT        NOT NULL,
    [EventType] VARCHAR (50)  NOT NULL,
    [Timestamp] DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK__StatusEv__3214EC0713596F78] PRIMARY KEY CLUSTERED ([Id] ASC)
);


