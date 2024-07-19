CREATE TABLE [dbo].[ResultsSpeech]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[RequestId] INT NOT NULL,
	[ResultTypeId] INT NULL,
	[SourceLangCode] VARCHAR(10) NULL,
	[TargetLangCode] VARCHAR(10) NULL,
	[AudioUrl] NVARCHAR(max) NULL,
	[AudioData] VARBINARY(max) NULL,
	[Translate] BIT NOT NULL Default 0,
	[Transcribe] BIT NOT NULL Default 0,
	[OutputAsAudio] BIT NOT NULL Default 0,
	[VoiceName] VARCHAR(100) NULL,
	[ResultText] NVARCHAR(MAX) NULL,
	[ResultAudio] VARBINARY(MAX) NULL,
	[CreatedDt] DATETIME NOT NULL, 
	[UpdatedDt] DATETIME NOT NULL, 
	[CompletedDt] DATETIME NULL,
    CONSTRAINT [FK_ResultsSpeech_RequestsSpeech] FOREIGN KEY ([RequestId]) REFERENCES [RequestsSpeech]([Id]),
	CONSTRAINT [FK_ResultsSpeech_ResultTypes] FOREIGN KEY ([ResultTypeId]) REFERENCES [ResultTypes]([Id])
)
