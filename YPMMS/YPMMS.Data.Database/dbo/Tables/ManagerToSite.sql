CREATE TABLE [dbo].[ManagerToSite] (
    [ManagerId] BIGINT NOT NULL,
    [SiteId]    BIGINT NOT NULL,
    CONSTRAINT [PK_ManagerToSite] PRIMARY KEY CLUSTERED ([ManagerId] ASC, [SiteId] ASC),
    CONSTRAINT [FK_ManagerToSite_Manager] FOREIGN KEY ([ManagerId]) REFERENCES [dbo].[Manager] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ManagerToSite_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

