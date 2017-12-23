CREATE TABLE [dbo].[SocketConnection] (
    [Id]        BIGINT        NOT NULL,
    [IpAddress] VARCHAR (200) NOT NULL,
    [Data]      VARCHAR (MAX) NOT NULL,
    [IsValid]   BIT           NOT NULL,
    CONSTRAINT [PK_SocketConnection] PRIMARY KEY CLUSTERED ([Id] ASC)
);

