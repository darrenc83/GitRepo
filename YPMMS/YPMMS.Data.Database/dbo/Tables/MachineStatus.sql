CREATE TABLE [dbo].[MachineStatus] (
    [Id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [MachineId] BIGINT        NOT NULL,
    [Status]    VARCHAR (50)  NOT NULL,
    [Timestamp] DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_MachineStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);

