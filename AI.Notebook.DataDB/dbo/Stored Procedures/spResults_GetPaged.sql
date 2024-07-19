CREATE PROCEDURE [dbo].[spResults_GetPaged]
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
		ResourceId int,
		ResultTypeId int, 
		DateCreated datetime, 
		DateUpdated datetime,
		DateCompleted datetime
	)
	--Populate table with content
	INSERT INTO #TempResults ( Id, ResourceId, ResultTypeId, DateCreated, DateUpdated, DateCompleted)
	Select res.Id, resc.Id as 'ResourceId', res.ResultTypeId, res.CreatedDt, res.UpdatedDt, res.CompletedDt 
	From ResultsVision as res
		Inner Join RequestsVision as req on res.RequestId = req.Id
		inner join AIResources as resc on req.ResourceId = resc.Id

	INSERT INTO #TempResults ( Id, ResourceId, ResultTypeId, DateCreated, DateUpdated, DateCompleted)
	Select res.Id, resc.Id as 'ResourceId', res.ResultTypeId, res.CreatedDt, res.UpdatedDt, res.CompletedDt
	From ResultsLanguage as res
		Inner Join RequestsLanguage as req on res.RequestId = req.Id
		inner join AIResources as resc on req.ResourceId = resc.Id

	INSERT INTO #TempResults ( Id, ResourceId, ResultTypeId, DateCreated, DateUpdated, DateCompleted)
	Select res.Id, resc.Id as 'ResourceId', res.ResultTypeId, res.CreatedDt, res.UpdatedDt, res.CompletedDt
	From ResultsSpeech as res
		Inner Join RequestsSpeech as req on res.RequestId = req.Id
		inner join AIResources as resc on req.ResourceId = resc.Id

	INSERT INTO #TempResults ( Id, ResourceId, ResultTypeId, DateCreated, DateUpdated, DateCompleted)
	Select res.Id, resc.Id as 'ResourceId', res.ResultTypeId, res.CreatedDt, res.UpdatedDt, res.CompletedDt
	From ResultsTranslator as res
		Inner Join RequestsTranslator as req on res.RequestId = req.Id
		inner join AIResources as resc on req.ResourceId = resc.Id

	SELECT @Total = Count(Id) FROM #TempResults;

	SELECT res.Id, res.ResultTypeId, res.ResourceId, res.DateCreated as 'CreatedDt', res.DateUpdated as 'UpdatedDt', res.DateCompleted as 'CompletedDt'
		FROM #TempResults AS res
		INNER JOIN dbo.AIResources as resc on res.ResourceId = resc.Id
		WHERE 
		(@Search IS NULL or (resc.[Name] LIKE '%' + @Search +'%' OR resc.[Name] LIKE '%' + @Search +'%'))
		AND 
		((@Begin IS NULL AND @End IS NULL) or res.DateCreated BETWEEN @Begin AND @End)
		ORDER BY 
			--CASE WHEN @SortBy = 'Name' AND @SortOrder = 'Asc' Then resc.[Name] END Asc,
			--CASE WHEN @SortBy = 'Name' AND @SortOrder = 'Desc' Then resc.[Name] END Desc,
			CASE WHEN @SortBy = 'Resource' AND @SortOrder = 'Asc' Then res.ResourceId END Asc,
			CASE WHEN @SortBy = 'Resource' AND @SortOrder = 'Desc' Then res.ResourceId END Desc,
			CASE WHEN @SortBy = 'Created' AND @SortOrder = 'Asc' Then 'CreatedDt' END Asc,
			CASE WHEN @SortBy = 'Created' AND @SortOrder = 'Desc' Then 'CreatedDt' END Desc,
			CASE WHEN @SortBy = 'Modified' AND @SortOrder = 'Asc' Then 'UpdatedDt' END Asc,
			CASE WHEN @SortBy = 'Modified' AND @SortOrder = 'Desc' Then 'UpdatedDt' END Desc,
			CASE WHEN @SortBy = 'Completed' AND @SortOrder = 'Asc' Then 'CompletedDt' END Asc,
			CASE WHEN @SortBy = 'Completed' AND @SortOrder = 'Desc' Then 'CompletedDt' END Desc
		OFFSET @Start ROWS
		FETCH NEXT @PageSize ROWS ONLY;
END