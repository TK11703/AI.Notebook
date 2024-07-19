CREATE PROCEDURE [dbo].[spRequestsSpeech_Update]
	@Id int,
	@Name nvarchar(50),	
	@SourceLangCode VARCHAR(10),
	@TargetLangCode VARCHAR(10),
	@AudioUrl NVARCHAR(max),
	@AudioData NVARCHAR(max),
	@Translate BIT,
	@Transcribe BIT,
	@OutputAsAudio BIT,
	@Ssml NVARCHAR(max),
	@SsmlUrl VARCHAR(255),
	@VoiceName VARCHAR(100)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY

		UPDATE dbo.RequestsSpeech
		SET 
		[Name] = @Name,
		[SourceLangCode] = @SourceLangCode,
		[TargetLangCode] = @TargetLangCode,
		[AudioUrl] = @AudioUrl,
		[AudioData] = @AudioData,
		[Translate] = @Translate,
		[Transcribe] = @Transcribe,
		[OutputAsAudio] = @OutputAsAudio,
		[VoiceName] = @VoiceName,
		[UpdatedDt] = GETDATE()
		WHERE [Id] = @Id;
	
		return 1;
	END TRY
	BEGIN CATCH
		
		return 0;
	END CATCH
END