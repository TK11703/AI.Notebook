CREATE PROCEDURE [dbo].[spRequestsTranslator_Delete]
	@Id int
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRANSACTION [Tran1]
	BEGIN TRY

		DELETE 
		FROM dbo.ResultsTranslator 
		WHERE RequestId in (SELECT Id FROM dbo.RequestsTranslator WHERE Id=@Id)

		DELETE 
		FROM dbo.RequestsTranslator
		WHERE Id=@Id;


		COMMIT TRANSACTION [Tran1]
		
		return 1;
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION [Tran1]
		
		return 0;
	END CATCH
END
