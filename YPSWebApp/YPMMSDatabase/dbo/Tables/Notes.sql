﻿CREATE TABLE [dbo].[Notes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Note] NVARCHAR(MAX) NOT NULL, 
    [CreatedBy] NVARCHAR(100) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL
)
