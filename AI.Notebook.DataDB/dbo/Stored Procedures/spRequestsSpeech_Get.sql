CREATE PROCEDURE [dbo].[spRequestsSpeech_Get]
	@Id int
AS
BEGIN
	select req.*
	from dbo.RequestsSpeech as req
	where req.Id=@Id;
END