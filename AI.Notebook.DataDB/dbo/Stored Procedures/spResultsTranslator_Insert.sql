CREATE PROCEDURE [dbo].[spResultsTranslator_Insert]
	@RequestId int,
	@SourceLangCode VARCHAR(10),
	@TargetLangCode VARCHAR(10),
	@Input NVARCHAR(max),
	@Translate BIT,
	@Transliterate BIT,
	@OutputAsAudio BIT,
	@VoiceName VARCHAR(100),
	@Id int output
AS
BEGIN
	INSERT INTO dbo.ResultsTranslator
		([RequestId], [SourceLangCode], [TargetLangCode], [Input], [Translate], [Transliterate], [OutputAsAudio], [VoiceName], [CreatedDt], [UpdatedDt])
	Values
		(@RequestId, @SourceLangCode, @TargetLangCode, @Input, @Translate, @Transliterate, @OutputAsAudio, @VoiceName, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
