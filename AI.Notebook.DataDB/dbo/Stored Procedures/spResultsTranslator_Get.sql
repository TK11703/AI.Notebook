CREATE PROCEDURE [dbo].[spResultsTranslator_Get]
	@Id int
AS
BEGIN
	select res.*
	from dbo.ResultsTranslator as res
	where res.Id=@Id;
END