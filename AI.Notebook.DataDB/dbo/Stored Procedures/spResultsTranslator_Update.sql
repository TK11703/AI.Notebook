CREATE PROCEDURE [dbo].[spResultsTranslator_Update]
	@Id int,
	@ResultTypeId int,
	@ResultText nvarchar(max),	
	@ResultAudio varbinary(max),
	@CompletedDt datetime
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY

		UPDATE dbo.ResultsTranslator
		SET 
			[ResultTypeId] = @ResultTypeId,
			[ResultAudio] = @ResultAudio,
			[ResultText] = @ResultText,
			[CompletedDt] = @CompletedDt,
			[UpdatedDt] = GETDATE()
		WHERE [Id] = @Id;
		
		return 1;
	END TRY
	BEGIN CATCH
		
		return 0;
	END CATCH
END