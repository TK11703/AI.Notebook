CREATE PROCEDURE [dbo].[spResultsTranslator_GetByRequest]
	@RequestId int
AS
BEGIN
	select res.Id, req.ResourceId, res.ResultTypeId, res.RequestId, res.CreatedDt, res.UpdatedDt, res.CompletedDt
	from dbo.ResultsTranslator as res
	Inner Join RequestsTranslator as req on res.RequestId = req.Id
	where res.RequestId=@RequestId;
END