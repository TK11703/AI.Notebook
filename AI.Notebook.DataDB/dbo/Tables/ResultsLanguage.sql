CREATE TABLE [dbo].[ResultsLanguage]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[RequestId] INT NOT NULL,
	[ResultTypeId] INT NULL,
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
	[ResultText] NVARCHAR(MAX) NULL,
	[CreatedDt] DATETIME NOT NULL, 
	[UpdatedDt] DATETIME NOT NULL, 
	[CompletedDt] DATETIME NULL,
    CONSTRAINT [FK_ResultsLanguage_RequestsLanguage] FOREIGN KEY ([RequestId]) REFERENCES [RequestsLanguage]([Id]),
	CONSTRAINT [FK_ResultsLanguage_ResultTypes] FOREIGN KEY ([ResultTypeId]) REFERENCES [ResultTypes]([Id])
)
