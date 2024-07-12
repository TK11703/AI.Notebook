CREATE PROCEDURE [dbo].[spResults_Get]
	@Id int
AS
BEGIN
	select res.*
	from dbo.Results as res
	where res.Id=@Id;
END
