﻿CREATE PROCEDURE [dbo].[spResultsSpeech_Delete]
	@Id int = 0
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
		
		Delete from dbo.ResultsSpeech
		where [Id] = @Id

		return 1;
	END TRY
	BEGIN CATCH
		
		return 0;
	END CATCH
END