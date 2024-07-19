using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.AIRequests.GetById;

public class GetAIRequestById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Requests/{{id}}", HandleAsync)
			.WithTags(Tags.AIRequests);
	}

	protected virtual async Task<Results<Ok<RequestBase>, ProblemHttpResult, NotFound>> HandleAsync(int id, IRequestData requestData)
	{
		try
		{
			var result = await requestData.GetAsync(id);
			if (result == null)
				return TypedResults.NotFound();
			return TypedResults.Ok(result);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
