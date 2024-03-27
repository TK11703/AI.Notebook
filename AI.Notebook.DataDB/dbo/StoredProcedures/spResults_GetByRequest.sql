CREATE PROCEDURE [dbo].[spResults_GetByRequest]
	@RequestId int
AS
BEGIN
	select res.*
	from dbo.Results as res
	where res.RequestId=@RequestId;
END
