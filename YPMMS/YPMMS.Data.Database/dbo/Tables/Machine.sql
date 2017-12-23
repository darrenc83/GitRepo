CREATE TABLE [dbo].[Machine] (
    [Id]                   BIGINT         NOT NULL,
    [Name]                 NVARCHAR (250) NOT NULL,
    [Currency]             VARCHAR (50)   NULL,
    [SystemType]           VARCHAR (50)   NOT NULL,
    [Status]               VARCHAR (50)   NOT NULL,
    [TcpUrl]               VARCHAR (50)   NULL,
    [TcpPort]              VARCHAR (5)    NULL,
    [UpdateAvailable]      TINYINT        CONSTRAINT [DF_Machine_UpdateAvailable] DEFAULT ((0)) NOT NULL,
    [AutoUpdate]           TINYINT        CONSTRAINT [DF_Machine_AutoUpdate] DEFAULT ((0)) NOT NULL,
    [LastConnectedTime]    DATETIME2 (7)  NULL,
    [LastCollectionTime]   DATETIME2 (7)  NULL,
    [LastCollectionAmount] MONEY          NULL,
    [Timestamp]            DATETIME2 (7)  CONSTRAINT [DF_Machine_Timestamp] DEFAULT (getutcdate()) NOT NULL,
    [SiteId]               BIGINT         NOT NULL,
    CONSTRAINT [PK_Machine] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Machine_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [UQ_Machine_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);





