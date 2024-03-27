CREATE PROCEDURE [dbo].[spAIResources_Get]
	@Id int
AS
BEGIN
	SELECT * 
	FROM dbo.AIResources
	WHERE [Id]=@Id AND [Active]=1;
END