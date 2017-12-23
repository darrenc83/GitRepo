CREATE TABLE [dbo].[Terminals]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TerminalType] NVARCHAR(100) NOT NULL, 
    [TerminalAmount] DECIMAL NOT NULL
)
