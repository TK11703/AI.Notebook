CREATE PROCEDURE [dbo].[spRequestsSpeech_Delete]
	@Id int
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRANSACTION [Tran1]
	BEGIN TRY

		DELETE 
		FROM dbo.ResultsSpeech 
		WHERE RequestId in (SELECT Id FROM dbo.RequestsSpeech WHERE Id=@Id)

		DELETE 
		FROM dbo.RequestsSpeech
		WHERE Id=@Id;

		COMMIT TRANSACTION [Tran1]
		
		return 1;
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION [Tran1]
		
		return 0;
	END CATCH
END
