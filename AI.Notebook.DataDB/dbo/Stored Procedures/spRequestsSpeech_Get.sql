CREATE PROCEDURE [dbo].[spRequestsSpeech_Get]
	@RequestId int
AS
BEGIN
	select req.*
	from dbo.RequestsSpeech as req
	where req.RequestId=@RequestId;
END