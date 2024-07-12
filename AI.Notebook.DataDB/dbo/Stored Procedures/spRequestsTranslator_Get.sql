CREATE PROCEDURE [dbo].[spRequestsTranslator_Get]
	@RequestId int
AS
BEGIN
	select req.*
	from dbo.RequestsTranslator as req
	where req.RequestId=@RequestId;
END