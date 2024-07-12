CREATE PROCEDURE [dbo].[spResultsLanguage_Get]
	@ResultId int
AS
BEGIN
	select res.*
	from dbo.ResultsLanguage as res
	where res.ResultId=@ResultId;
END