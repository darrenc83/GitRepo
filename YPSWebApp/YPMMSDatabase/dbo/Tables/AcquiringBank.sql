CREATE TABLE [dbo].[AcquiringBank] (
    [Id]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [Bank] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_AcquringBank] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AcquringBank_AcquringBank] FOREIGN KEY ([Id]) REFERENCES [dbo].[AcquiringBank] ([Id])
);

