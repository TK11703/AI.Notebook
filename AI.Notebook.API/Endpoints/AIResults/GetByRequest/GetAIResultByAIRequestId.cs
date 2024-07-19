using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.AITypedResults.GetByRequest;

public class GetAIResultByAIRequestId : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Results/Request/{{requestId}}", HandleAsync)
			.WithTags(Tags.AIResults);
	}

	protected virtual async Task<Results<Ok<IEnumerable<ResultBase>>, ProblemHttpResult>> HandleAsync(int requestId, IResultData resultData)
	{
		try
		{
			return TypedResults.Ok(await resultData.GetByRequestAsync(requestId));
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
