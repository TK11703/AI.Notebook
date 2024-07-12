CREATE PROCEDURE [dbo].[spRequests_GetAll]
AS
BEGIN
	select req.*
	from dbo.Requests as req;
END