CREATE PROCEDURE [dbo].[spResultsVision_Insert]
	@RequestId int,
	@ImageUrl NVARCHAR(max),
	@ImageData varbinary(max),
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
		([RequestId], [ImageUrl], [ImageData], [GenderNeutralCaption], [Caption], [DenseCaptions], [Tags], [ObjectDetection], [SmartCrop], [People], [Ocr], [CreatedDt], [UpdatedDt])
	Values
		(@RequestId, @ImageUrl, @ImageData, @GenderNeutralCaption, @Caption, @DenseCaptions, @Tags, @ObjectDetection, @SmartCrop, @People, @Ocr, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
