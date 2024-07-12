CREATE PROCEDURE [dbo].[spRequestsLanguage_Update]
	@Name nvarchar(50),
	@RequestId int,
	@SourceLangCode VARCHAR(10),
	@TargetLangCode VARCHAR(10),
	@Input NVARCHAR(max),
	@Language BIT,
	@Sentiment BIT,
	@KeyPhrases BIT,
	@Entities BIT,
	@PiiEntities BIT,
	@LinkedEntities BIT,
	@NamedEntityRecognition BIT,
	@Summary BIT,
	@AbstractiveSummary BIT
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

		UPDATE dbo.RequestsLanguage
		SET 
		[SourceLangCode] = @SourceLangCode,
		[TargetLangCode] = @TargetLangCode,
		[Input] = @Input,
		[Language] = @Language,
		[Sentiment] = @Sentiment,
		[KeyPhrases] = @KeyPhrases,
		[Entities] = @Entities,
		[PiiEntities] = @PiiEntities,
		[LinkedEntities] = @LinkedEntities,
		[NamedEntityRecognition] = @NamedEntityRecognition,
		[Summary] = @Summary,
		[AbstractiveSummary] = @AbstractiveSummary,
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