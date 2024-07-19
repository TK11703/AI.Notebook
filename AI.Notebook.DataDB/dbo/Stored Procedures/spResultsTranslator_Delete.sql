CREATE PROCEDURE [dbo].[spResultsTranslator_Delete]
	@ResultId int,
	@Id int = 0
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRANSACTION [Tran1]
	BEGIN TRY

		Delete from dbo.ResultsTranslator
		where [Id] = @Id

		Delete from dbo.Results
		where [Id] = @ResultId

		COMMIT TRANSACTION [Tran1]

		return 1;
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION [Tran1]
		
		return 0;
	END CATCH
END