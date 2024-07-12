CREATE PROCEDURE [dbo].[spAIResources_Update]
	@Id int,
	@Name nvarchar(150),
	@Description nvarchar(250),
	@Active bit
AS
BEGIN
	SET NOCOUNT OFF;

	UPDATE dbo.AIResources
	SET [Name]=@Name, [Description]=@Description, [Active]=@Active
	WHERE [Id]=@Id;
END
