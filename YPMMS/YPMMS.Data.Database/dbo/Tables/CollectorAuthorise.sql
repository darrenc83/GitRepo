CREATE TABLE [dbo].[CollectorAuthorise] (
    [Id]        BIGINT IDENTITY (1, 1) NOT NULL,
    [KeyId]     BIGINT NOT NULL,
    [MachineId] BIGINT NOT NULL,
    CONSTRAINT [PK_CollectorAuthorise] PRIMARY KEY CLUSTERED ([Id] ASC)
);

