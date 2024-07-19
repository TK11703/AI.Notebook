CREATE PROCEDURE [dbo].[spResultsSpeech_Delete]
	@ResultId int,
	@Id int = 0
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRANSACTION [Tran1]
	BEGIN TRY
		Delete from dbo.Results
		where [Id] = @ResultId

		Delete from dbo.ResultsSpeech
		where [Id] = @Id

		COMMIT TRANSACTION [Tran1]

		return 1;
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION [Tran1]
		
		return 0;
	END CATCH
END