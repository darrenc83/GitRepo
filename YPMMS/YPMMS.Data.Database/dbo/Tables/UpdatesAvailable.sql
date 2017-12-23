CREATE TABLE [dbo].[UpdatesAvailable] (
    [Id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [MachineId] BIGINT        NOT NULL,
    [UpdateId]  BIGINT        NOT NULL,
    [Timestamp] DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_UpdatesAvailable] PRIMARY KEY CLUSTERED ([Id] ASC)
);

