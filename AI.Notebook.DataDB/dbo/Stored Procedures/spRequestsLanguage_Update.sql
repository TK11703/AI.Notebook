CREATE PROCEDURE [dbo].[spRequestsLanguage_Update]
	@Id int,
	@Name nvarchar(50),	
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
	BEGIN TRY

		UPDATE dbo.RequestsLanguage
		SET 
		[Name] = @Name,
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
		WHERE [Id] = @Id;

		return 1;
	END TRY
	BEGIN CATCH
				
		return 0;
	END CATCH
END