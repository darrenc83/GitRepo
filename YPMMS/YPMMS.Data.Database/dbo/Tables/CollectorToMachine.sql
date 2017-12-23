CREATE TABLE [dbo].[CollectorToMachine] (
    [CollectorId]   BIGINT        NOT NULL,
    [MachineId]     BIGINT        NOT NULL,
    [CollectStatus] VARCHAR (50)  NULL,
    [LastUpdate]    DATETIME2 (7) NULL,
    CONSTRAINT [PK_CollectorToMachine] PRIMARY KEY CLUSTERED ([CollectorId] ASC, [MachineId] ASC),
    CONSTRAINT [FK_CollectorToMachine_Collector] FOREIGN KEY ([CollectorId]) REFERENCES [dbo].[Collector] ([Id]),
    CONSTRAINT [FK_CollectorToMachine_Machine] FOREIGN KEY ([MachineId]) REFERENCES [dbo].[Machine] ([Id])
);

