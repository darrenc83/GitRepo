CREATE TABLE [dbo].[Collection] (
    [Id]                BIGINT       IDENTITY (1, 1) NOT NULL,
    [CollectionEventId] BIGINT       NOT NULL,
    [CurrencyType]      VARCHAR (10) NOT NULL,
    [Currency]          VARCHAR (10) NOT NULL,
    [Value]             MONEY        NOT NULL,
    [Level]             INT          NOT NULL,
    CONSTRAINT [PK_Collection] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Collection_CollectionEvent] FOREIGN KEY ([CollectionEventId]) REFERENCES [dbo].[CollectionEvent] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

