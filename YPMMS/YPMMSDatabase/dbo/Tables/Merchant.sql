CREATE TABLE [dbo].[Merchant] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [MerchantId]      BIGINT            NOT NULL,
    [MerchantName]    NVARCHAR (MAX) NOT NULL,
    [CreatedDate]     DATETIME       NOT NULL,
    [CreatedBy]       NVARCHAR (MAX) NOT NULL,
    [LastUpdatedDate] DATETIME       NULL,
    [LastUpdatedBy]   NVARCHAR (MAX) NULL,
    [AcquiringBankId] BIGINT         NULL,
    [SellerId]        BIGINT         NULL,
    CONSTRAINT [PK_dbo.Merchant] PRIMARY KEY CLUSTERED ([MerchantId] ASC),
    CONSTRAINT [FK_Merchant_AcquiringBank] FOREIGN KEY ([AcquiringBankId]) REFERENCES [dbo].[AcquiringBank] ([Id]),
    CONSTRAINT [FK_Merchant_Seller] FOREIGN KEY ([SellerId]) REFERENCES [dbo].[Sellers] ([Id])
);

