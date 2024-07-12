CREATE PROCEDURE [dbo].[spResultsLanguage_Insert]
	@ResultId int,
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
	@AbstractiveSummary BIT,
	@Id int output
AS
BEGIN
	INSERT INTO dbo.ResultsLanguage
	([ResultId], [SourceLangCode],	[TargetLangCode], [Input], [Language], [Sentiment], [KeyPhrases], [Entities], [PiiEntities], [LinkedEntities], 
		[NamedEntityRecognition], [Summary], [AbstractiveSummary], [CreatedDt], [UpdatedDt])
	Values
	(@ResultId, @SourceLangCode, @TargetLangCode, @Input, @Language, @Sentiment, @KeyPhrases, @Entities, @PiiEntities, @LinkedEntities, 
		@NamedEntityRecognition, @Summary, @AbstractiveSummary, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
