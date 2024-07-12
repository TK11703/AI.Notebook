CREATE PROCEDURE [dbo].[spResultTypes_GetAll]

AS
BEGIN
	SELECT * 
	FROM dbo.ResultTypes
	WHERE [Active]=1
	ORDER BY [Name];
END