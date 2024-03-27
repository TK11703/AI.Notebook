﻿CREATE TABLE [dbo].[ResultTypes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(250) NULL,
	[Active] BIT NOT NULL Default 1
)
