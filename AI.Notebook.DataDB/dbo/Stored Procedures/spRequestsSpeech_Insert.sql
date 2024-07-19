CREATE PROCEDURE [dbo].[spRequestsSpeech_Insert]
	@Name nvarchar(50),
	@SourceLangCode VARCHAR(10),
	@TargetLangCode VARCHAR(10),
	@AudioUrl NVARCHAR(max),
	@AudioData NVARCHAR(max),
	@Translate BIT,
	@Transcribe BIT,
	@OutputAsAudio BIT,
	@VoiceName VARCHAR(100),
	@Id int output
AS
BEGIN
	INSERT INTO dbo.RequestsSpeech
		([Name], [SourceLangCode], [TargetLangCode], [AudioUrl], [AudioData], [Translate], [Transcribe], [OutputAsAudio], [VoiceName], [CreatedDt], [UpdatedDt])
	Values
		(@Name, @SourceLangCode, @TargetLangCode, @AudioUrl, @AudioData, @Translate, @Transcribe, @OutputAsAudio, @VoiceName, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
