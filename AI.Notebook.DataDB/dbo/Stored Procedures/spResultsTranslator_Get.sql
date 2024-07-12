CREATE PROCEDURE [dbo].[spResultsTranslator_Get]
	@ResultId int
AS
BEGIN
	select res.*
	from dbo.ResultsTranslator as res
	where res.ResultId=@ResultId;
END