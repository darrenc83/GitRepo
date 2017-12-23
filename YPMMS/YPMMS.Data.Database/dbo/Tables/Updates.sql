CREATE TABLE [dbo].[Updates] (
    [Id]             BIGINT          IDENTITY (1, 1) NOT NULL,
    [Device]         VARCHAR (50)    NOT NULL,
    [Version]        VARCHAR (50)    NOT NULL,
    [Path]           VARCHAR (120)   NOT NULL,
    [FTPFileName]    VARCHAR (50)    NULL,
    [FileData]       VARBINARY (MAX) NULL,
    [FileLength]     BIGINT          NULL,
    [MD5Hash]        VARCHAR (50)    NOT NULL,
    [PublishedToFTP] INT             CONSTRAINT [DF_Updates_PublishedToFTP] DEFAULT ((0)) NULL,
    [Enabled]        INT             CONSTRAINT [DF_Updates_Enabled] DEFAULT ((0)) NOT NULL,
    [Timestamp]      DATETIME2 (7)   NOT NULL,
    CONSTRAINT [PK_Updates] PRIMARY KEY CLUSTERED ([Id] ASC)
);





