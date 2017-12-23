CREATE TABLE [dbo].[Site] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (100) NOT NULL,
    [Address]   NVARCHAR (MAX) NOT NULL,
    [Town]      NVARCHAR (100) NULL,
    [Area]      NVARCHAR (100) NULL,
    [Postcode]  VARCHAR (10)   NOT NULL,
    [Country]   NVARCHAR (100) NOT NULL,
    [Latitude]  FLOAT (53)     NOT NULL,
    [Longitude] FLOAT (53)     NOT NULL,
    CONSTRAINT [PK_Site] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Site_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

