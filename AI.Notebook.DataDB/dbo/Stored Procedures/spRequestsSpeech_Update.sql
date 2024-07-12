CREATE PROCEDURE [dbo].[spRequestsSpeech_Update]
	@Name nvarchar(50),
	@RequestId int,
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
	BEGIN TRANSACTION [Tran1]
	BEGIN TRY

		UPDATE dbo.Requests
		SET
		[Name] = @Name,
		[UpdatedDt] = GETDATE()
		WHERE [Id] = @RequestId;

		UPDATE dbo.RequestsSpeech
		SET 
		[SourceLangCode] = @SourceLangCode,
		[TargetLangCode] = @TargetLangCode,
		[AudioUrl] = @AudioUrl,
		[AudioData] = @AudioData,
		[Translate] = @Translate,
		[Transcribe] = @Transcribe,
		[OutputAsAudio] = @OutputAsAudio,
		[Ssml] = @Ssml,
		[SsmlUrl] = @SsmlUrl,
		[VoiceName] = @VoiceName,
		[UpdatedDt] = GETDATE()
		WHERE [RequestId] = @RequestId;

		COMMIT TRANSACTION [Tran1]
		
		return 1;
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION [Tran1]
		
		return 0;
	END CATCH
END