CREATE TABLE [dbo].[Collector] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserId]        NVARCHAR (128) NULL,
    [KeyId]         BIGINT         NOT NULL,
    [Country]       VARCHAR (50)   NOT NULL,
    [CollectStatus] VARCHAR (50)   NULL,
    [MachineId]     BIGINT         NULL,
    [LastUpdate]    DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Collector] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Collector_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Collector_Machine] FOREIGN KEY ([MachineId]) REFERENCES [dbo].[Machine] ([Id]),
    CONSTRAINT [UQ_Collector_KeyId] UNIQUE NONCLUSTERED ([KeyId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Collector_UserId_uindex]
    ON [dbo].[Collector]([UserId] ASC);

