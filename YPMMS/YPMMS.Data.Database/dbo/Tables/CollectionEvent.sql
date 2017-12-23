CREATE TABLE [dbo].[CollectionEvent] (
    [Id]             BIGINT        IDENTITY (1, 1) NOT NULL,
    [CollectorId]    BIGINT        NOT NULL,
    [MachineId]      BIGINT        NOT NULL,
    [CollectionType] VARCHAR (50)  NOT NULL,
    [Timestamp]      DATETIME2 (7) CONSTRAINT [DF__Collectio__Times__6C190EBB] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK__CollectionEvent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CollectionEvent_Collector] FOREIGN KEY ([CollectorId]) REFERENCES [dbo].[Collector] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_CollectionEvent_Machine] FOREIGN KEY ([MachineId]) REFERENCES [dbo].[Machine] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);



GO
CREATE NONCLUSTERED INDEX [IX_CollectionEvent_Timestamp]
    ON [dbo].[CollectionEvent]([Timestamp] ASC);

