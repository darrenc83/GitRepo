CREATE TABLE [dbo].[MachineManifest] (
    [Id]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [MachineId]     BIGINT        NOT NULL,
    [Device]        VARCHAR (50)  NOT NULL,
    [Version]       VARCHAR (100) NOT NULL,
    [BuildRevision] VARCHAR (50)  NULL,
    [Timestamp]     DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_MachineManifest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MachineManifest_Machine] FOREIGN KEY ([MachineId]) REFERENCES [dbo].[Machine] ([Id])
);

