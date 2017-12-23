CREATE TABLE [dbo].[CollectorAction] (
    [Id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [MachineId]   BIGINT        NOT NULL,
    [KeyId]       BIGINT        NOT NULL,
    [Action]      VARCHAR (50)  NOT NULL,
    [Permission]  VARCHAR (50)  NOT NULL,
    [Transmitted] DATETIME2 (7) NULL,
    [Added]       DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_CollectorAction] PRIMARY KEY CLUSTERED ([Id] ASC)
);

