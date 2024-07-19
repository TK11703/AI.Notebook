CREATE PROCEDURE [dbo].[spRequestsTranslator_Insert]
	@Name nvarchar(50),
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
	INSERT INTO dbo.RequestsTranslator
		([Name], [SourceLangCode], [TargetLangCode], [Input], [Translate], [Transliterate], [OutputAsAudio], [VoiceName], [CreatedDt], [UpdatedDt])
	Values
		(@Name, @SourceLangCode, @TargetLangCode, @Input, @Translate, @Transliterate, @OutputAsAudio, @VoiceName, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
