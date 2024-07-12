CREATE TABLE [dbo].[ResultsLanguage]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[ResultId] INT NOT NULL,
	[SourceLangCode] VARCHAR(10) NULL,
	[TargetLangCode] VARCHAR(10) NULL,
	[Input] NVARCHAR(max) NULL,
	[Language] BIT NOT NULL Default 0,
	[Sentiment] BIT NOT NULL Default 0,
	[KeyPhrases] BIT NOT NULL Default 0,
	[Entities] BIT NOT NULL Default 0,
	[PiiEntities] BIT NOT NULL Default 0,
	[LinkedEntities] BIT NOT NULL Default 0,
	[NamedEntityRecognition] BIT NOT NULL Default 0,
	[Summary] BIT NOT NULL Default 0,
	[AbstractiveSummary] BIT NOT NULL Default 0,
	[CreatedDt] DATETIME NOT NULL, 
	[UpdatedDt] DATETIME NOT NULL, 
    CONSTRAINT [FK_ResultsLanguage_Results] FOREIGN KEY ([ResultId]) REFERENCES [Results]([Id])
)
