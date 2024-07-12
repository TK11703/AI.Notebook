﻿CREATE TABLE [dbo].[RequestsSpeech]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[RequestId] INT NOT NULL,
	[SourceLangCode] VARCHAR(10) NULL,
	[TargetLangCode] VARCHAR(10) NULL,
	[AudioUrl] NVARCHAR(max) NULL,
	[AudioData] NVARCHAR(max) NULL,
	[Translate] BIT NOT NULL Default 0,
	[Transcribe] BIT NOT NULL Default 0,
	[OutputAsAudio] BIT NOT NULL Default 0,
	[Ssml] NVARCHAR(max) NULL,
	[SsmlUrl] VARCHAR(255) NULL,
	[VoiceName] VARCHAR(100) NULL,
	[CreatedDt] DATETIME NOT NULL, 
	[UpdatedDt] DATETIME NOT NULL, 
    CONSTRAINT [FK_RequestsSpeech_Requests] FOREIGN KEY ([RequestId]) REFERENCES [Requests]([Id])
)