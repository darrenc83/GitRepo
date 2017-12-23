CREATE TABLE [dbo].[Sellers] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [SellersName] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Sellers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

