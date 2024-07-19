CREATE PROCEDURE [dbo].[spResultsVision_GetByRequest]
	@RequestId int
AS
BEGIN
	select res.Id, req.ResourceId, res.ResultTypeId, res.RequestId, res.CreatedDt, res.UpdatedDt, res.CompletedDt
	from dbo.ResultsVision as res
	Inner Join RequestsVision as req on res.RequestId = req.Id
	where res.RequestId=@RequestId;
END