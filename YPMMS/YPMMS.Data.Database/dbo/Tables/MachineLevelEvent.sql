CREATE TABLE [dbo].[MachineLevelEvent] (
    [Id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [MachineId] BIGINT        NOT NULL,
    [Timestamp] DATETIME2 (7) DEFAULT (getutcdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MachineLevelEvent_Machine] FOREIGN KEY ([MachineId]) REFERENCES [dbo].[Machine] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

