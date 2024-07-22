CREATE PROCEDURE [dbo].[spRequestsTranslator_Update]
	@Id int,
	@Name nvarchar(50),
	@SourceLangCode VARCHAR(10),
	@SourceScriptCode VARCHAR(10),
	@TargetLangCode VARCHAR(10),
	@TargetScriptCode VARCHAR(10),
	@Input NVARCHAR(max),
	@Translate BIT,
	@Transliterate BIT,
	@OutputAsAudio BIT,
	@VoiceName VARCHAR(100)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY

		UPDATE dbo.RequestsTranslator
		SET 
		[Name] = @Name,
		[SourceLangCode] = @SourceLangCode,
		[SourceScriptCode] = @SourceScriptCode,
		[TargetLangCode] = @TargetLangCode,
		[TargetScriptCode] = @TargetScriptCode,
		[Input] = @Input,
		[Translate] = @Translate,
		[Transliterate] = @Transliterate,
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