CREATE TABLE [dbo].[MerchantNotes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MerchantId] BIGINT NOT NULL, 
    [NoteId] INT NOT NULL, 
    CONSTRAINT [FK_MerchantNotes_ToMerchant] FOREIGN KEY (MerchantId) REFERENCES Merchant(MerchantId), 
    CONSTRAINT [FK_MerchantNotes_ToNotes] FOREIGN KEY (NoteId) REFERENCES Notes(Id)
)
