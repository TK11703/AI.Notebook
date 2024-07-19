CREATE PROCEDURE [dbo].[spRequestsVision_Update]
	@Id int,
	@Name nvarchar(50),
	@ImageUrl NVARCHAR(max),
	@ImageData varbinary(max),
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
	BEGIN TRY

		UPDATE dbo.RequestsVision
		SET 
		[Name] = @Name,
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
		WHERE [Id] = @Id;

		
		return 1;
	END TRY
	BEGIN CATCH
		
		return 0;
	END CATCH
END