CREATE PROCEDURE [dbo].[spResultsLanguage_Insert]
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
	@AbstractiveSummary BIT,
	@ResultTypeId int = null,
	@ResultText nvarchar(max) = null,
	@CompletedDt datetime = null,
	@Id int output
AS
BEGIN
	INSERT INTO dbo.ResultsLanguage
		([RequestId], [SourceLangCode],	[TargetLangCode], [Input], [Language], [Sentiment], [KeyPhrases], [Entities], [PiiEntities], [LinkedEntities], 
			[NamedEntityRecognition], [Summary], [AbstractiveSummary], [CreatedDt], [UpdatedDt], [ResultTypeId], [ResultText], [CompletedDt])
	Values
		(@RequestId, @SourceLangCode, @TargetLangCode, @Input, @Language, @Sentiment, @KeyPhrases, @Entities, @PiiEntities, @LinkedEntities, 
			@NamedEntityRecognition, @Summary, @AbstractiveSummary, GETDATE(), GETDATE(), @ResultTypeId, @ResultText, @CompletedDt);

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
