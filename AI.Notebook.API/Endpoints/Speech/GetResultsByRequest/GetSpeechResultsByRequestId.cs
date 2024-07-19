using Microsoft.AspNetCore.Http.HttpResults;
namespace AI.Notebook.API.Endpoints.Speech.GetResultsByRequest;

public class GetSpeechResultsByRequestId : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Speech/Results/Request/{{requestId}}", HandleAsync)
			.WithTags(Tags.Speech);
	}

	protected virtual async Task<Results<Ok<IEnumerable<ResultBase>>, ProblemHttpResult>> HandleAsync(int requestId, IResultData resultData)
	{
		try
		{
			return TypedResults.Ok(await resultData.GetSpeechResultsByRequestAsync(requestId));
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
