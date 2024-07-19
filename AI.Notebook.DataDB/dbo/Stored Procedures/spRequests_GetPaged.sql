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
	--Drop table is hanging around
	DROP TABLE IF EXISTS #TempResults;
	--Create table with expected results
	Create Table #TempResults 
	(
		Id int, 
		[Name] varchar(50),
		ResourceId int,
		DateCreated datetime, 
		DateUpdated datetime
	)
	--Populate table with content
	INSERT INTO #TempResults ( Id, [Name], ResourceId, DateCreated, DateUpdated	)
	Select req.Id, req.[Name] as 'Name', resc.Id as 'ResourceId', req.CreatedDt, req.UpdatedDt 
	From RequestsVision as req
		inner join AIResources as resc on req.ResourceId = resc.Id

	INSERT INTO #TempResults ( Id, [Name], ResourceId, DateCreated, DateUpdated	)
	Select req.Id, req.[Name] as 'Name', resc.Id as 'ResourceId', req.CreatedDt, req.UpdatedDt 
	From RequestsLanguage as req
		inner join AIResources as resc on req.ResourceId = resc.Id

	INSERT INTO #TempResults ( Id, [Name], ResourceId, DateCreated, DateUpdated	)
	Select req.Id, req.[Name] as 'Name', resc.Id as 'ResourceId', req.CreatedDt, req.UpdatedDt 
	From RequestsSpeech as req
		inner join AIResources as resc on req.ResourceId = resc.Id

	INSERT INTO #TempResults ( Id, [Name], ResourceId, DateCreated, DateUpdated	)
	Select req.Id, req.[Name] as 'Name', resc.Id as 'ResourceId', req.CreatedDt, req.UpdatedDt  
	From RequestsTranslator as req
		inner join AIResources as resc on req.ResourceId = resc.Id

	SELECT @Total = Count(Id) FROM #TempResults;

	SELECT req.Id, req.[Name], req.ResourceId, req.DateCreated as 'CreatedDt', req.DateUpdated as 'UpdatedDt'
		FROM #TempResults AS req
		INNER JOIN dbo.AIResources as res on req.ResourceId = res.Id
		WHERE 
		(@Search IS NULL or (req.[Name] LIKE '%' + @Search +'%' OR req.[Name] LIKE '%' + @Search +'%'))
		AND 
		((@Begin IS NULL AND @End IS NULL) or req.DateCreated BETWEEN @Begin AND @End)
		ORDER BY 
			CASE WHEN @SortBy = 'Name' AND @SortOrder = 'Asc' Then req.[Name] END Asc,
			CASE WHEN @SortBy = 'Name' AND @SortOrder = 'Desc' Then req.[Name] END Desc,
			CASE WHEN @SortBy = 'Resource' AND @SortOrder = 'Asc' Then req.ResourceId END Asc,
			CASE WHEN @SortBy = 'Resource' AND @SortOrder = 'Desc' Then req.ResourceId END Desc,
			CASE WHEN @SortBy = 'Created' AND @SortOrder = 'Asc' Then 'CreatedDt' END Asc,
			CASE WHEN @SortBy = 'Created' AND @SortOrder = 'Desc' Then 'CreatedDt' END Desc,
			CASE WHEN @SortBy = 'Modified' AND @SortOrder = 'Asc' Then 'UpdatedDt' END Asc,
			CASE WHEN @SortBy = 'Modified' AND @SortOrder = 'Desc' Then 'UpdatedDt' END Desc
		OFFSET @Start ROWS
		FETCH NEXT @PageSize ROWS ONLY;
END