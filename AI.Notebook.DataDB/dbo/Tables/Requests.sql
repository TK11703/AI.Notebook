CREATE TABLE [dbo].[Requests]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[ResourceId] INT NOT NULL, 
	[Name] NVARCHAR(50) NOT NULL,
	[CreatedDt] DATETIME NOT NULL, 
	[UpdatedDt] DATETIME NOT NULL, 
    CONSTRAINT [FK_Requests_AIResources] FOREIGN KEY ([ResourceId]) REFERENCES [AIResources]([Id])
)
