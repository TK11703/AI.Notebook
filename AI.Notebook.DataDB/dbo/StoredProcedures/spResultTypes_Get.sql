CREATE PROCEDURE [dbo].[spResultTypes_Get]
	@Id int
AS
BEGIN
	SELECT * 
	FROM dbo.ResultTypes
	WHERE [Id]=@Id AND [Active]=1;
END