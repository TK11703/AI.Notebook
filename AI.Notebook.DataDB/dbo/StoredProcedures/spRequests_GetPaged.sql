CREATE PROCEDURE [dbo].[spRequests_GetPaged]
	@SortBy nvarchar(50),
	@SortOrder nvarchar(50),
	@Start int,
	@PageSize int,
	@Search nvarchar(150) = null,
	@Begin date = null,
	@End date = null,
	@Total int output
AS
BEGIN
	SELECT @Total = Count(req.Id) 
		FROM dbo.Requests AS req;

	SELECT req.*
		FROM dbo.Requests AS req
		INNER JOIN dbo.AIResources as res on req.ResourceId = res.Id
		WHERE 
		(@Search IS NULL or (req.Input LIKE '%' + @Search +'%' OR req.[Name] LIKE '%' + @Search +'%'))
		AND 
		((@Begin IS NULL AND @End IS NULL) or req.CreatedDt BETWEEN @Begin AND @End)
		ORDER BY 
			CASE WHEN @SortBy = 'Name' AND @SortOrder = 'Asc' Then req.[Name] END Asc,
			CASE WHEN @SortBy = 'Name' AND @SortOrder = 'Desc' Then req.[Name] END Desc,
			CASE WHEN @SortBy = 'Resource' AND @SortOrder = 'Asc' Then res.[Name] END Asc,
			CASE WHEN @SortBy = 'Resource' AND @SortOrder = 'Desc' Then res.[Name] END Desc,
			CASE WHEN @SortBy = 'Created' AND @SortOrder = 'Asc' Then req.CreatedDt END Asc,
			CASE WHEN @SortBy = 'Created' AND @SortOrder = 'Desc' Then req.CreatedDt END Desc,
			CASE WHEN @SortBy = 'Modified' AND @SortOrder = 'Asc' Then req.UpdatedDt END Asc,
			CASE WHEN @SortBy = 'Modified' AND @SortOrder = 'Desc' Then req.UpdatedDt END Desc
		OFFSET @Start ROWS
		FETCH NEXT @PageSize ROWS ONLY;
END