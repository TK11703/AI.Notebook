using Microsoft.AspNetCore.Http.HttpResults;
namespace AI.Notebook.API.Endpoints.Vision.GetResultsByRequest;

public class GetVisionResultsByRequestId : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Vision/Results/Request/{{requestId}}", HandleAsync)
			.WithTags(Tags.Vision);
	}

	protected virtual async Task<Results<Ok<IEnumerable<ResultBase>>, ProblemHttpResult>> HandleAsync(int requestId, IResultData resultData)
	{
		try
		{
			return TypedResults.Ok(await resultData.GetVisionResultsByRequestAsync(requestId));
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
