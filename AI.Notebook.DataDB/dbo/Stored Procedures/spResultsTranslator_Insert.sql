CREATE PROCEDURE [dbo].[spResultsTranslator_Insert]
	@ResultId int,
	@SourceLangCode VARCHAR(10),
	@TargetLangCode VARCHAR(10),
	@Input NVARCHAR(max),
	@Translate BIT,
	@Transliterate BIT,
	@OutputAsAudio BIT,
	@Ssml NVARCHAR(max),
	@SsmlUrl VARCHAR(255),
	@VoiceName VARCHAR(100),
	@Id int output
AS
BEGIN
	INSERT INTO dbo.ResultsTranslator
	([ResultId], [SourceLangCode], [TargetLangCode], [Input], [Translate], [Transliterate], [OutputAsAudio], [Ssml], [SsmlUrl], [VoiceName], [CreatedDt], [UpdatedDt])
	Values
	(@ResultId, @SourceLangCode, @TargetLangCode, @Input, @Translate, @Transliterate, @OutputAsAudio, @Ssml, @SsmlUrl, @VoiceName, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
