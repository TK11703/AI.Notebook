﻿CREATE PROCEDURE [dbo].[spRequestsVision_Get]
	@Id int
AS
BEGIN
	select req.*
	from dbo.RequestsVision as req
	where req.Id=@Id;
END