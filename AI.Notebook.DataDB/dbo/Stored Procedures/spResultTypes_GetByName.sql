CREATE PROCEDURE [dbo].[spResultTypes_GetByName]
	@Name varchar(100)
AS
BEGIN
	SELECT * 
	FROM dbo.ResultTypes
	WHERE [Name]=@Name AND [Active]=1;
END