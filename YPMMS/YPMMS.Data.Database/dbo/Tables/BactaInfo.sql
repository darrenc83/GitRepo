CREATE TABLE [dbo].[BactaInfo] (
    [Id]                BIGINT        IDENTITY (1, 1) NOT NULL,
    [MachineId]         BIGINT        NOT NULL,
    [ManufacturerIdent] VARCHAR (10)  NULL,
    [Protocol]          VARCHAR (10)  NULL,
    [MachineIdent]      VARCHAR (10)  NULL,
    [SoftwareVersion]   VARCHAR (10)  NULL,
    [MoneyType]         VARCHAR (10)  NULL,
    [TargetPayout]      VARCHAR (10)  NULL,
    [MachineType]       VARCHAR (30)  NULL,
    [PriceOfPlay]       VARCHAR (30)  NULL,
    [CoinSystem]        VARCHAR (30)  NULL,
    [JackpotValue]      VARCHAR (30)  NULL,
    [DataPort]          VARCHAR (6)   NULL,
    [DevicesFitted]     VARCHAR (200) NULL,
    [Timestamp]         DATETIME2 (7) NULL
);

