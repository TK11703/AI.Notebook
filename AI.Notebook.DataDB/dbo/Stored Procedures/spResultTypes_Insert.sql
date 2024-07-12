CREATE PROCEDURE [dbo].[spResultTypes_Insert]
	@Name nvarchar(150),
	@Description nvarchar(250),
	@Active bit = 1,
	@Id int output
AS
BEGIN
	IF EXISTS (Select [Id] FROM dbo.ResultTypes WHERE [Name]=@Name AND [Active]=0)
	BEGIN
		SELECT @Id = [Id] 
			FROM dbo.ResultTypes 
			WHERE [Name]=@Name AND [Active]=0;
		UPDATE dbo.ResultTypes
			SET [Active] = 1, [Description]=@Description
			WHERE [Id]=@Id;

		return 1;
	END
	ELSE IF NOT EXISTS(Select [Id] from dbo.ResultTypes WHERE [Name]=@Name AND [Active]=1)
	BEGIN
		INSERT INTO dbo.ResultTypes
		([Name], [Description], [Active])
		VALUES (@Name, @Description, @Active);

		SET @Id = SCOPE_IDENTITY();

		return 1;
	END
	ELSE
	BEGIN
		SET @Id = 0;
		return 0;
	END
END

