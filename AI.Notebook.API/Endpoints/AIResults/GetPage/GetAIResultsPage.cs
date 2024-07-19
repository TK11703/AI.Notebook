using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AI.Notebook.API.Endpoints.AIResults.GetPage;

public class GetAIResultsPage : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Results/PagedRequest", HandleAsync)
			.AddEndpointFilter<ValidatorFilter<PageRequest>>()
			.WithTags(Tags.AIResults);
	}

	protected virtual async Task<Results<Ok<PageResult<ResultBase>>, ProblemHttpResult>> HandleAsync([FromBody] PageRequest pageRequest, IResultData resultData)
	{
		try
		{
			return TypedResults.Ok(await resultData.GetPagedAsync(pageRequest));
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
