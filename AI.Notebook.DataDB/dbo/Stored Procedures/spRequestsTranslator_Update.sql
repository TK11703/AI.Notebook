CREATE PROCEDURE [dbo].[spRequestsTranslator_Update]
	@Name nvarchar(50),
	@RequestId int,
	@SourceLangCode VARCHAR(10),
	@TargetLangCode VARCHAR(10),
	@Input NVARCHAR(max),
	@Translate BIT,
	@Transliterate BIT,
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

		UPDATE dbo.RequestsTranslator
		SET 
		[SourceLangCode] = @SourceLangCode,
		[TargetLangCode] = @TargetLangCode,
		[Input] = @Input,
		[Translate] = @Translate,
		[Transliterate] = @Transliterate,
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