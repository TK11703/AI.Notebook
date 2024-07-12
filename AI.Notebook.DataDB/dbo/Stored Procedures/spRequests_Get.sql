CREATE PROCEDURE [dbo].[spRequests_Get]
	@Id int
AS
BEGIN
	select req.*
	from dbo.Requests as req
	where req.Id=@Id;
END
