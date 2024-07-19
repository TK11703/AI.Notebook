CREATE PROCEDURE [dbo].[spResultsLanguage_GetByRequest]
	@RequestId int
AS
BEGIN
	select res.Id, req.ResourceId, res.ResultTypeId, res.RequestId, res.CreatedDt, res.UpdatedDt, res.CompletedDt
	from dbo.ResultsLanguage as res
	Inner Join RequestsLanguage as req on res.RequestId = req.Id
	where res.RequestId=@RequestId;
END