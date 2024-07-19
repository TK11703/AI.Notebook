CREATE TABLE [dbo].[RequestsVision]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[Name] NVARCHAR(50) NOT NULL,
	[ResourceId] INT NOT NULL, 
	[ImageUrl] NVARCHAR(max) NULL,
	[ImageData] varbinary(max) NULL,
	[GenderNeutralCaption] BIT NOT NULL Default 0,
	[Caption] BIT NOT NULL Default 0,
	[DenseCaptions] BIT NOT NULL Default 0,
	[Tags] BIT NOT NULL Default 0,
	[ObjectDetection] BIT NOT NULL Default 0,
	[SmartCrop] BIT NOT NULL Default 0,
	[People] BIT NOT NULL Default 0,
	[Ocr] BIT NOT NULL Default 0,
	[CreatedDt] DATETIME NOT NULL, 
	[UpdatedDt] DATETIME NOT NULL, 
    CONSTRAINT [FK_RequestsVision_AIResources] FOREIGN KEY ([ResourceId]) REFERENCES [AIResources]([Id])
)
