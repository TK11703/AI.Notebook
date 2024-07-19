CREATE PROCEDURE [dbo].[spResultsSpeech_GetByRequest]
	@RequestId int
AS
BEGIN
	select res.Id, req.ResourceId, res.ResultTypeId, res.RequestId, res.CreatedDt, res.UpdatedDt, res.CompletedDt
	from dbo.ResultsSpeech as res
	Inner Join RequestsSpeech as req on res.RequestId = req.Id
	where res.RequestId=@RequestId;
END