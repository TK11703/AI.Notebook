CREATE PROCEDURE [dbo].[spResultsSpeech_Insert]
	@RequestId int,
	@SourceLangCode VARCHAR(10),
	@TargetLangCode VARCHAR(10),
	@AudioUrl NVARCHAR(max),
	@AudioData varbinary(max),
	@Translate BIT,
	@Transcribe BIT,
	@OutputAsAudio BIT,	
	@VoiceName VARCHAR(100),
	@Id int output
AS
BEGIN
	INSERT INTO dbo.ResultsSpeech
		([RequestId], [SourceLangCode], [TargetLangCode], [AudioUrl], [AudioData], [Translate], [Transcribe], [OutputAsAudio], [VoiceName], [CreatedDt], [UpdatedDt])
	Values
		(@RequestId, @SourceLangCode, @TargetLangCode, @AudioUrl, @AudioData, @Translate, @Transcribe, @OutputAsAudio, @VoiceName, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
