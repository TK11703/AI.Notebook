using Microsoft.AspNetCore.Http.HttpResults;
namespace AI.Notebook.API.Endpoints.Language.GetResultsByRequest;

public class GetLanguageResultsByRequestId : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Language/Results/Request/{{requestId}}", HandleAsync)
			.WithTags(Tags.Language);
	}

	protected virtual async Task<Results<Ok<IEnumerable<ResultBase>>, ProblemHttpResult>> HandleAsync(int requestId, IResultData resultData)
	{
		try
		{
			return TypedResults.Ok(await resultData.GetLanguageResultsByRequestAsync(requestId));
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
