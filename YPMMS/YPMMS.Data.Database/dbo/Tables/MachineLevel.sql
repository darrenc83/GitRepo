CREATE TABLE [dbo].[MachineLevel] (
    [Id]                  BIGINT       IDENTITY (1, 1) NOT NULL,
    [MachineLevelEventId] BIGINT       NOT NULL,
    [Type]                VARCHAR (10) NOT NULL,
    [Currency]            VARCHAR (6)  NOT NULL,
    [LevelType]           VARCHAR (30) NOT NULL,
    [Level]               INT          NOT NULL,
    [Value]               MONEY        NOT NULL,
    CONSTRAINT [PK_Table1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MachineLevel_MachineLevelEvent] FOREIGN KEY ([MachineLevelEventId]) REFERENCES [dbo].[MachineLevelEvent] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);



