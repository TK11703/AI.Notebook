CREATE PROCEDURE [dbo].[spRequestsVision_Insert]
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
	@Ocr BIT,
	@Id int output
AS
BEGIN
	INSERT INTO dbo.RequestsVision
		([Name], [ImageUrl], [ImageData], [GenderNeutralCaption], [Caption], [DenseCaptions], [Tags], [ObjectDetection], [SmartCrop], [People], [Ocr], [CreatedDt], [UpdatedDt])
	Values
		(@Name, @ImageUrl, @ImageData, @GenderNeutralCaption, @Caption, @DenseCaptions, @Tags, @ObjectDetection, @SmartCrop, @People, @Ocr, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
