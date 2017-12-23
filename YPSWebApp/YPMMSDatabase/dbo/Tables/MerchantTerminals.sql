CREATE TABLE [dbo].[MerchantTerminals]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MerchantId] BIGINT NOT NULL, 
    [TerminalId] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    [Total] DECIMAL NOT NULL, 
    CONSTRAINT [FK_MerchantTerminals_ToMerchant] FOREIGN KEY (MerchantId) REFERENCES Merchant(MerchantId), 
    CONSTRAINT [FK_MerchantTerminals_ToTerminals] FOREIGN KEY (TerminalId) REFERENCES Terminals(Id)
)
