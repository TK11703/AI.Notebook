CREATE TABLE [dbo].[Results]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[RequestId] INT NOT NULL,
	[ResourceId] INT NOT NULL, 
	[ResultTypeId] INT NOT NULL, 
	[ResultData] NVARCHAR(MAX) NULL,
	[CreatedDt] DATETIME NOT NULL, 
	[UpdatedDt] DATETIME NOT NULL,
	[CompletedDt] DATETIME,
	CONSTRAINT [FK_Results_Requests] FOREIGN KEY ([RequestId]) REFERENCES [Requests]([Id]),
	CONSTRAINT [FK_Results_AIResources] FOREIGN KEY ([ResourceId]) REFERENCES [AIResources]([Id]),
	CONSTRAINT [FK_Results_ResultTypes] FOREIGN KEY ([ResultTypeId]) REFERENCES [ResultTypes]([Id]),
)
