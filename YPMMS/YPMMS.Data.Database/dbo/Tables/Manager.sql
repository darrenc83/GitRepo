CREATE TABLE [dbo].[Manager] (
    [Id]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserId] NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Manager_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

