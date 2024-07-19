CREATE PROCEDURE [dbo].[spRequestsLanguage_Get]
	@Id int
AS
BEGIN
	select req.*
	from dbo.RequestsLanguage as req
	where req.Id=@Id;
END