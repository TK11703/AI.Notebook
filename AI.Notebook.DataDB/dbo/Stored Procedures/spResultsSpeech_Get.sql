CREATE PROCEDURE [dbo].[spResultsSpeech_Get]
	@ResultId int
AS
BEGIN
	select res.*
	from dbo.ResultsSpeech as res
	where res.ResultId=@ResultId;
END