CREATE PROCEDURE [dbo].[spRequestsTranslator_Get]
	@Id int
AS
BEGIN
	select req.*
	from dbo.RequestsTranslator as req
	where req.Id=@Id;
END