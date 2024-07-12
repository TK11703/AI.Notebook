CREATE PROCEDURE [dbo].[spRequests_Insert]
	@ResourceId int,
	@Name nvarchar(50),
	@Id int output
AS
BEGIN
	INSERT INTO dbo.Requests
	([ResourceId], [Name], [CreatedDt], [UpdatedDt])
	Values
	(@ResourceId, @Name, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
