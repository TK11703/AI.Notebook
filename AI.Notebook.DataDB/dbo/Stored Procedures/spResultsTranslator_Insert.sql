CREATE PROCEDURE [dbo].[spResultsTranslator_Insert]
	@RequestId int,
	@SourceLangCode VARCHAR(10),
	@TargetLangCode VARCHAR(10),
	@Input NVARCHAR(max),
	@Translate BIT,
	@Transliterate BIT,
	@OutputAsAudio BIT,
	@VoiceName VARCHAR(100),
	@ResultTypeId int = null,
	@ResultText nvarchar(max) = null,	
	@ResultAudio varbinary(max),
	@CompletedDt datetime = null,
	@Id int output
AS
BEGIN
	INSERT INTO dbo.ResultsTranslator
		([RequestId], [SourceLangCode], [TargetLangCode], [Input], [Translate], [Transliterate], [OutputAsAudio], [VoiceName], [CreatedDt], [UpdatedDt], [ResultTypeId], [ResultText], [ResultAudio], [CompletedDt])
	Values
		(@RequestId, @SourceLangCode, @TargetLangCode, @Input, @Translate, @Transliterate, @OutputAsAudio, @VoiceName, GETDATE(), GETDATE(), @ResultTypeId, @ResultText, @ResultAudio, @CompletedDt);

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
