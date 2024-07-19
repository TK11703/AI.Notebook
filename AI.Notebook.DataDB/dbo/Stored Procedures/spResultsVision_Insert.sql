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
	@ResultTypeId int = null,
	@ResultText nvarchar(max) = null,	
	@CompletedDt datetime = null,
	@Id int output
AS
BEGIN
	INSERT INTO dbo.ResultsVision
		([RequestId], [ImageUrl], [ImageData], [GenderNeutralCaption], [Caption], [DenseCaptions], [Tags], [ObjectDetection], [SmartCrop], [People], 
			[Ocr], [CreatedDt], [UpdatedDt], [ResultTypeId], [ResultText], [CompletedDt])
	Values
		(@RequestId, @ImageUrl, @ImageData, @GenderNeutralCaption, @Caption, @DenseCaptions, @Tags, @ObjectDetection, @SmartCrop, @People, 
			@Ocr, GETDATE(), GETDATE(), @ResultTypeId, @ResultText, @CompletedDt);

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
