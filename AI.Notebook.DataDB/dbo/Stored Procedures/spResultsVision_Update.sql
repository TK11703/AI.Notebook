﻿CREATE PROCEDURE [dbo].[spResultsVision_Update]
	@Id int,
	@ResultTypeId int,
	@ResultText nvarchar(max),	
	@CompletedDt datetime
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY

		UPDATE dbo.ResultsVision
		SET 
			[ResultTypeId] = @ResultTypeId,
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