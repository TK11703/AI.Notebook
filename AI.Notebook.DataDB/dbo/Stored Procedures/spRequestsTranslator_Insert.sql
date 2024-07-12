CREATE PROCEDURE [dbo].[spRequestsTranslator_Insert]
	@RequestId int,
	@Id int output
AS
BEGIN
	INSERT INTO dbo.RequestsTranslator
	([RequestId], [CreatedDt], [UpdatedDt])
	Values
	(@RequestId, GETDATE(), GETDATE());

	SET @Id = SCOPE_IDENTITY();

	return 1;
END
