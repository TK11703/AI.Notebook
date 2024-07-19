CREATE PROCEDURE [dbo].[spRequestsLanguage_Insert]
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
	@AbstractiveSummary BIT,
	@Id int output
AS
BEGIN
	INSERT INTO dbo.RequestsLanguage
		([Name], [Input], [SourceLangCode], [TargetLangCode], [Language], [Sentiment], [KeyPhrases],[Entities], [PiiEntities], [LinkedEntities], 
			[NamedEntityRecognition], [Summary], [AbstractiveSummary], [CreatedDt], [UpdatedDt])
	Values	
		(@Name, @Input, @SourceLangCode, @TargetLangCode, @Language, @Sentiment, @KeyPhrases, @Entities, @PiiEntities, @LinkedEntities, 
			@NamedEntityRecognition, @Summary, @AbstractiveSummary, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END		
