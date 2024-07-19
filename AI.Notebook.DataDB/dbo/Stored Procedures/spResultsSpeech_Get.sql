CREATE PROCEDURE [dbo].[spResultsSpeech_Get]
	@Id int
AS
BEGIN
	select res.*
	from dbo.ResultsSpeech as res
	where res.Id=@Id;
END