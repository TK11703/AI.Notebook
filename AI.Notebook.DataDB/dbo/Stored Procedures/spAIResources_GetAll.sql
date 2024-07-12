CREATE PROCEDURE [dbo].[spAIResources_GetAll]

AS
BEGIN
	SELECT * 
	FROM dbo.AIResources
	WHERE [Active]=1
	ORDER BY [Name];
END