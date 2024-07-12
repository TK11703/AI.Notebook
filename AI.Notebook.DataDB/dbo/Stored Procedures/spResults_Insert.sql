CREATE PROCEDURE [dbo].[spResults_Insert]
	@RequestId int,
	@ResourceId int,
	@ResultTypeId int,
	@CompletedDt datetime = null,
	@ResultData varbinary(max),
	@Id int output
AS
BEGIN
	INSERT INTO dbo.Results
	([RequestId], [ResourceId], [ResultTypeId], [ResultData], [CompletedDt], [CreatedDt], [UpdatedDt])
	Values
	(@RequestId, @ResourceId, @ResultTypeId, @ResultData, @CompletedDt, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
