CREATE PROCEDURE [dbo].[spResultsVision_Delete]
	@Id int = 0
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
		Delete from dbo.ResultsVision
		where [Id] = @Id

		return 1;
	END TRY
	BEGIN CATCH
		
		return 0;
	END CATCH
END