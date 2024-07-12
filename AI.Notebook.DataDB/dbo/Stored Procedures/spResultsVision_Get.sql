CREATE PROCEDURE [dbo].[spResultsVision_Get]
	@ResultId int
AS
BEGIN
	select res.*
	from dbo.ResultsVision as res
	where res.ResultId=@ResultId;
END