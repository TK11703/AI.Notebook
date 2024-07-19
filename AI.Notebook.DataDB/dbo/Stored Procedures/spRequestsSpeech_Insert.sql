CREATE PROCEDURE [dbo].[spRequestsSpeech_Insert]
	@ResourceId int,
	@Name nvarchar(50),
	@SourceLangCode VARCHAR(10),
	@TargetLangCode VARCHAR(10),
	@AudioUrl NVARCHAR(max),
	@AudioData VARBINARY(max),
	@Translate BIT,
	@Transcribe BIT,
	@OutputAsAudio BIT,
	@VoiceName VARCHAR(100),
	@Id int output
AS
BEGIN
	INSERT INTO dbo.RequestsSpeech
		([ResourceId], [Name], [SourceLangCode], [TargetLangCode], [AudioUrl], [AudioData], [Translate], [Transcribe], [OutputAsAudio], [VoiceName], [CreatedDt], [UpdatedDt])
	Values
		(@ResourceId, @Name, @SourceLangCode, @TargetLangCode, @AudioUrl, @AudioData, @Translate, @Transcribe, @OutputAsAudio, @VoiceName, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
