CREATE PROCEDURE [dbo].[spRequests_Update]
	@Id int,
	@ResourceId int,
	@Name nvarchar(50),
	@Input nvarchar(max)
AS
BEGIN
	SET NOCOUNT OFF;

	UPDATE dbo.Requests
	SET [ResourceId] = @ResourceId, [Name] = @Name, [Input] = @Input, [UpdatedDt] = GETDATE()
	WHERE [Id] = @Id;
END
