CREATE PROCEDURE [dbo].[spRequests_Delete]
	@Id int
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRANSACTION [Tran1]
	BEGIN TRY
		Delete 
		FROM dbo.Results
		WHERE RequestId=@Id;

		Delete 
		FROM dbo.RequestsTranslator
		WHERE RequestId=@Id;

		Delete 
		FROM dbo.RequestsSpeech
		WHERE RequestId=@Id;

		Delete 
		FROM dbo.RequestsVision
		WHERE RequestId=@Id;

		Delete 
		FROM dbo.RequestsLanguage
		WHERE RequestId=@Id;

		Delete 
		FROM dbo.Requests	
		WHERE Id=@Id;

		COMMIT TRANSACTION [Tran1]
		
		return 1;
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION [Tran1]
		
		return 0;
	END CATCH
END
