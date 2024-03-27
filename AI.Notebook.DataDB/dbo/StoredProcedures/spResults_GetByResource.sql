CREATE PROCEDURE [dbo].[spResults_GetByResource]
	@ResourceId int
AS
BEGIN
	select res.*
	from dbo.Results as res
	where res.ResourceId=@ResourceId;
END
