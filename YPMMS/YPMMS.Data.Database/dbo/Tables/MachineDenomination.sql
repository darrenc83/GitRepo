CREATE TABLE [dbo].[MachineDenomination] (
    [Id]           BIGINT       IDENTITY (1, 1) NOT NULL,
    [MachineId]    BIGINT       NOT NULL,
    [Type]         VARCHAR (10) NOT NULL,
    [Currency]     VARCHAR (6)  NOT NULL,
    [Value]        MONEY        NOT NULL,
    [FloatLevel]   INT          NOT NULL,
    [CashboxLevel] INT          NOT NULL,
    [StoredLevel]  INT          NOT NULL,
    CONSTRAINT [PK_MachineDenomination] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MachineDenomination_Machine] FOREIGN KEY ([MachineId]) REFERENCES [dbo].[Machine] ([Id])
);

