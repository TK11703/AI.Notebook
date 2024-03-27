CREATE PROCEDURE [dbo].[spRequests_Insert]
	@ResourceId int,
	@Name nvarchar(50),
	@Input nvarchar(max),
	@Id int output
AS
BEGIN
	INSERT INTO dbo.Requests
	([ResourceId], [Name], [Input], [CreatedDt], [UpdatedDt])
	Values
	(@ResourceId, @Name, @Input, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
