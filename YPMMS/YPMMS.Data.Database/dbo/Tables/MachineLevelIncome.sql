CREATE TABLE [dbo].[MachineLevelIncome] (
    [id]         BIGINT        NULL,
    [MachineId]  BIGINT        NULL,
    [Timestamp]  DATETIME2 (7) NULL,
    [Stored]     MONEY         NULL,
    [CashBox]    MONEY         NULL,
    [Profitloss] MONEY         NULL,
    [Accum]      MONEY         NULL,
    [nYear]      INT           NULL,
    [nMonth]     INT           NULL,
    [nDay]       INT           NULL,
    [nHour]      INT           NULL,
    [float]      INT           NULL
);

