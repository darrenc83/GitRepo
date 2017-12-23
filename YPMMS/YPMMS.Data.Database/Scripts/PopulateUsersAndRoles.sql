USE [live-collect]
GO


-- ROLES

-- Admin
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7fcff573-1019-44c8-a197-0e919426ef8b', N'Admin')
GO

-- Collector
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'eb2f5b2d-1c35-46e8-a021-9784e6004bae', N'Collector')
GO

-- Manager
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6fd95c11-92a1-4dce-a1c4-e2d326894562', N'Manager')
GO


-- USERS

-- USER     admin
-- PASSWORD Admin123
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name])
VALUES (N'5663c1b3-e8f4-44be-8db5-199a77002c96', N'admin@test.com', 0, N'AKJM3rp8sskflxtaq0Uji3l8IHlXpuHQdP2uWB8ZseUdw7Fjp7BMSfXfb+h7AxGnIA==', N'e6bf48b7-6e42-4b8c-a9cd-11d07a883a9a', NULL, 0, 0, NULL, 1, 0, N'admin', N'Admin')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5663c1b3-e8f4-44be-8db5-199a77002c96', N'7fcff573-1019-44c8-a197-0e919426ef8b')
GO

-- USER     collector1
-- PASSWORD Collector123
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name])
VALUES (N'b54eed6a-d9bc-49cf-ba6e-362f56c979b9', N'collector1@test.com', 0, N'AO6AjeUoSzgbdssmdX7sdG+j4LERiBGM9CG+gvDALTuzPtCzj8BZnlRqk+qiyPFWjw==', N'1bf47112-2ea2-4d68-8fa0-b11c5c04b9ef', NULL, 0, 0, NULL, 1, 0, N'collector1', N'Collector 1')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b54eed6a-d9bc-49cf-ba6e-362f56c979b9', N'eb2f5b2d-1c35-46e8-a021-9784e6004bae')
GO

-- USER     collector2
-- PASSWORD Collector123
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name])
VALUES (N'b24f5070-ed5e-4421-bb1a-a08f008a9bc2', N'collector2@test.com', 0, N'AHJuSyyv1qwnCwdg6zf3qw0SnraNB4SJCYsa5TNgdOxyoKOV9oweDOA/WwZ0Nn0yUQ==', N'936f8a6b-e131-4945-8025-c6bf5583d1ca', NULL, 0, 0, NULL, 1, 0, N'collector2', N'Collector 2')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b24f5070-ed5e-4421-bb1a-a08f008a9bc2', N'eb2f5b2d-1c35-46e8-a021-9784e6004bae')
GO

-- USER     collector3
-- PASSWORD Collector123
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name])
VALUES (N'fd379dd9-b8a1-4c31-b85b-ff9906aaf2a6', N'collector3@test.com', 0, N'AOSqlhT7MjQhUS1RrfojG0yyO2gjyyb/yQg5v6mUSbU/7SnQfLoRR7/9aQYtz6ZIAg==', N'018e8cde-62aa-43ed-9104-d466c9dac7e8', NULL, 0, 0, NULL, 1, 0, N'collector3', N'Collector 3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fd379dd9-b8a1-4c31-b85b-ff9906aaf2a6', N'eb2f5b2d-1c35-46e8-a021-9784e6004bae')
GO

-- USER     manager1
-- PASSWORD Manager123
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name])
VALUES (N'8498301b-f2d2-4d4c-a91a-38802d2b9926', N'manager1@test.com', 0, N'AOgmKi6qhcZC64M0zfhOypJCYlT3gkqJ225P/mcVdyrHSxHHoZhYmsgPSu/8FwmsMw==', N'888f1da3-aa46-49b5-a3ec-dd8280f3fd41', NULL, 0, 0, NULL, 1, 0, N'manager1', N'Manager 1')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8498301b-f2d2-4d4c-a91a-38802d2b9926', N'6fd95c11-92a1-4dce-a1c4-e2d326894562')
GO

