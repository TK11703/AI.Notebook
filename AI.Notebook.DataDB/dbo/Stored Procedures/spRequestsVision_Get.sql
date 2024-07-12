CREATE PROCEDURE [dbo].[spRequestsVision_Get]
	@RequestId int
AS
BEGIN
	select req.*
	from dbo.RequestsVision as req
	where req.RequestId=@RequestId;
END