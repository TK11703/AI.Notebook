CREATE TABLE [dbo].[ResultsTranslator]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[RequestId] INT NOT NULL,
	[ResultTypeId] INT NULL, 
	[SourceLangCode] VARCHAR(10) NULL,
	[TargetLangCode] VARCHAR(10) NULL,
	[Input] NVARCHAR(MAX) NULL,
	[Translate] BIT NOT NULL Default 0,
	[Transliterate] BIT NOT NULL Default 0,
	[OutputAsAudio] BIT NOT NULL Default 0,
	[VoiceName] VARCHAR(100) NULL,
	[ResultText] NVARCHAR(MAX) NULL,
	[ResultAudio] VARBINARY(MAX) NULL,
	[CreatedDt] DATETIME NOT NULL, 
	[UpdatedDt] DATETIME NOT NULL, 
	[CompletedDt] DATETIME NULL,
    CONSTRAINT [FK_ResultsTranslator_RequestsTranslator] FOREIGN KEY ([RequestId]) REFERENCES [RequestsTranslator]([Id]),
	CONSTRAINT [FK_ResultsTranslator_ResultTypes] FOREIGN KEY ([ResultTypeId]) REFERENCES [ResultTypes]([Id])
)
