CREATE PROCEDURE [dbo].[spResultsSpeech_Insert]
	@ResultId int,
	@SourceLangCode VARCHAR(10),
	@TargetLangCode VARCHAR(10),
	@AudioUrl NVARCHAR(max),
	@AudioData NVARCHAR(max),
	@Translate BIT,
	@Transcribe BIT,
	@OutputAsAudio BIT,
	@Ssml NVARCHAR(max),
	@SsmlUrl VARCHAR(255),
	@VoiceName VARCHAR(100),
	@Id int output
AS
BEGIN
	INSERT INTO dbo.ResultsSpeech
	([ResultId], [SourceLangCode], [TargetLangCode], [AudioUrl], [AudioData], [Translate], [Transcribe], [OutputAsAudio], [Ssml], [SsmlUrl], [VoiceName], [CreatedDt], [UpdatedDt])
	Values
	(@ResultId, @SourceLangCode, @TargetLangCode, @AudioUrl, @AudioData, @Translate, @Transcribe, @OutputAsAudio, @Ssml, @SsmlUrl, @VoiceName, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
