CREATE TABLE [dbo].[DebugLog] (
    [id]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [MachineId]    BIGINT        NULL,
    [Description]  NCHAR (255)   NULL,
    [RecTimestamp] DATETIME2 (7) NULL,
    [GetTimestamp] DATETIME2 (7) NULL
);

