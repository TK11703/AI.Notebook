CREATE PROCEDURE [dbo].[spResultsVision_Get]
	@Id int
AS
BEGIN
	select res.*
	from dbo.ResultsVision as res
	where res.Id=@Id;
END