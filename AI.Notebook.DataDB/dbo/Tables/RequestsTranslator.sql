CREATE TABLE [dbo].[RequestsTranslator]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[Name] NVARCHAR(50) NOT NULL,
	[ResourceId] INT NOT NULL, 
	[SourceLangCode] VARCHAR(10) NULL,
	[TargetLangCode] VARCHAR(10) NULL,
	[Input] NVARCHAR(max) NULL,
	[Translate] BIT NOT NULL Default 0,
	[Transliterate] BIT NOT NULL Default 0,
	[OutputAsAudio] BIT NOT NULL Default 0,
	[VoiceName] VARCHAR(100) NULL,
	[CreatedDt] DATETIME NOT NULL, 
	[UpdatedDt] DATETIME NOT NULL, 
    CONSTRAINT [FK_RequestsTranslator_AIResources] FOREIGN KEY ([ResourceId]) REFERENCES [AIResources]([Id])
)
