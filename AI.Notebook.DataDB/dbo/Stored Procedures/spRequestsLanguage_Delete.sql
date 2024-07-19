CREATE PROCEDURE [dbo].[spRequestsLanguage_Delete]
	@Id int
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRANSACTION [Tran1]
	BEGIN TRY

		DELETE 
		FROM dbo.ResultsLanguage 
		WHERE RequestId in (SELECT Id FROM dbo.RequestsLanguage WHERE Id=@Id)

		DELETE 
		FROM dbo.RequestsLanguage
		WHERE Id=@Id;

		COMMIT TRANSACTION [Tran1]
		
		return 1;
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION [Tran1]
		
		return 0;
	END CATCH
END
