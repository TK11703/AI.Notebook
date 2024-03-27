CREATE TABLE [dbo].[AIResources]
(
	[Id] INT NOT NULL PRIMARY KEY identity,
	[Name] NVARCHAR(150) NOT NULL,
	[Description] NVARCHAR(250) NULL,
	[Active] BIT NOT NULL Default 1
)
