CREATE PROCEDURE [dbo].[spRequestsLanguage_Get]
	@RequestId int
AS
BEGIN
	select req.*
	from dbo.RequestsLanguage as req
	where req.RequestId=@RequestId;
END