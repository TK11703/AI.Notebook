CREATE PROCEDURE [dbo].[spResults_Update]
	@Id int,
	@RequestId int,
	@ResourceId int,
	@ResultTypeId int,
	@CompletedDt datetime = null,
	@ResultData varbinary(max)
AS
BEGIN
	SET NOCOUNT OFF;

	UPDATE dbo.Results
	SET [RequestId] = @RequestId,
		[ResourceId] = @ResourceId, 
		[ResultTypeId] = @ResultTypeId, 
		[ResultData] = @ResultData, 
		[CompletedDt] = @CompletedDt,
		[UpdatedDt] = GETDATE()
	WHERE [Id] = @Id;
END
