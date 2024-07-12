CREATE PROCEDURE [dbo].[spRequests_Update]
	@Id int,
	@ResourceId int,
	@Name nvarchar(50)
AS
BEGIN
	SET NOCOUNT OFF;

	UPDATE dbo.Requests
	SET [ResourceId] = @ResourceId, [Name] = @Name, [UpdatedDt] = GETDATE()
	WHERE [Id] = @Id;
END
