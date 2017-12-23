CREATE TABLE [dbo].[TebsRecord] (
    [Id]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [MachineId]  BIGINT        NOT NULL,
    [Tebscode]   VARCHAR (50)  NULL,
    [RecordType] VARCHAR (30)  NOT NULL,
    [RejectCode] VARCHAR (30)  NOT NULL,
    [KeyId]      BIGINT        NULL,
    [Country]    VARCHAR (10)  NOT NULL,
    [Value]      MONEY         NOT NULL,
    [Timestamp]  DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_TebsRecord] PRIMARY KEY CLUSTERED ([Id] ASC)
);



