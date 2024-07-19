using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AI.Notebook.API.Endpoints.AIRequests.GetPage;

public class GetAIRequestsPage : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Requests/PagedRequest", HandleAsync)
			.AddEndpointFilter<ValidatorFilter<PageRequest>>()
			.WithTags(Tags.AIRequests);
	}

	protected virtual async Task<Results<Ok<PageResult<RequestBase>>, ProblemHttpResult>> HandleAsync([FromBody] PageRequest pageRequest, IRequestData requestData)
	{
		try
		{
			return TypedResults.Ok(await requestData.GetPagedAsync(pageRequest));
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
