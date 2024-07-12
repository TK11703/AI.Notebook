CREATE PROCEDURE [dbo].[spResultsVision_Insert]
	@ResultId int,
	@ImageUrl NVARCHAR(max),
	@ImageData NVARCHAR(max),
	@GenderNeutralCaption BIT,
	@Caption BIT,
	@DenseCaptions BIT,
	@Tags BIT,
	@ObjectDetection BIT,
	@SmartCrop BIT,
	@People BIT,
	@Ocr BIT,
	@Id int output
AS
BEGIN
	INSERT INTO dbo.ResultsVision
	([ResultId], [ImageUrl], [ImageData], [GenderNeutralCaption], [Caption], [DenseCaptions], [Tags], [ObjectDetection], [SmartCrop], [People], [Ocr], [CreatedDt], [UpdatedDt])
	Values
	(@ResultId, @ImageUrl, @ImageData, @GenderNeutralCaption, @Caption, @DenseCaptions, @Tags, @ObjectDetection, @SmartCrop, @People, @Ocr, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
