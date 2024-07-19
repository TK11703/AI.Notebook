CREATE PROCEDURE [dbo].[spResultsLanguage_Get]
	@Id int
AS
BEGIN
	select res.*
	from dbo.ResultsLanguage as res
	where res.Id=@Id;
END