CREATE TABLE [dbo].[ResultsVision]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[RequestId] INT NOT NULL,
	[ResultTypeId] INT NULL,
	[ImageUrl] NVARCHAR(max) NULL,
	[ImageData] VARBINARY(max) NULL,
	[GenderNeutralCaption] BIT NOT NULL Default 0,
	[Caption] BIT NOT NULL Default 0,
	[DenseCaptions] BIT NOT NULL Default 0,
	[Tags] BIT NOT NULL Default 0,
	[ObjectDetection] BIT NOT NULL Default 0,
	[SmartCrop] BIT NOT NULL Default 0,
	[People] BIT NOT NULL Default 0,
	[Ocr] BIT NOT NULL Default 0,
	[ResultText] NVARCHAR(MAX) NULL,
	[CreatedDt] DATETIME NOT NULL, 
	[UpdatedDt] DATETIME NOT NULL, 
	[CompletedDt] DATETIME NOT NULL,
    CONSTRAINT [FK_ResultsVision_RequestsVision] FOREIGN KEY ([RequestId]) REFERENCES [RequestsVision]([Id]),
	CONSTRAINT [FK_ResultsVision_ResultTypes] FOREIGN KEY ([ResultTypeId]) REFERENCES [ResultTypes]([Id])
)
