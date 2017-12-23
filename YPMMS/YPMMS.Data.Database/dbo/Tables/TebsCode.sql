CREATE TABLE [dbo].[TebsCode] (
    [Id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [MachineId] BIGINT        NOT NULL,
    [Code]      VARCHAR (50)  NULL,
    [Timestamp] DATETIME2 (7) NOT NULL
);

