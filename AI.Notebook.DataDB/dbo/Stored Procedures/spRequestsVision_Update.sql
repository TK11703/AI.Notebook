CREATE PROCEDURE [dbo].[spRequestsVision_Update]
	@Name nvarchar(50),
	@RequestId int,
	@ImageUrl NVARCHAR(max),
	@ImageData NVARCHAR(max),
	@GenderNeutralCaption BIT,
	@Caption BIT,
	@DenseCaptions BIT,
	@Tags BIT,
	@ObjectDetection BIT,
	@SmartCrop BIT,
	@People BIT,
	@Ocr BIT
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRANSACTION [Tran1]
	BEGIN TRY
		
		UPDATE dbo.Requests
		SET
		[Name] = @Name,
		[UpdatedDt] = GETDATE()
		WHERE [Id] = @RequestId;

		UPDATE dbo.RequestsVision
		SET 
		[ImageUrl] = @ImageUrl,
		[ImageData] = @ImageData,
		[GenderNeutralCaption] = @GenderNeutralCaption,
		[Caption] = @Caption,
		[DenseCaptions] = @DenseCaptions,
		[Tags] = @Tags,
		[ObjectDetection] = @ObjectDetection,
		[SmartCrop] = @SmartCrop,
		[People] = @People,
		[Ocr] = @Ocr,
		[UpdatedDt] = GETDATE()
		WHERE [RequestId] = @RequestId;

		COMMIT TRANSACTION [Tran1]
		
		return 1;
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION [Tran1]
		
		return 0;
	END CATCH
END