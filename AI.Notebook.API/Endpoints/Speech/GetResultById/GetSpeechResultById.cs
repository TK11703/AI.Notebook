using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Speech.GetResultById;

public class GetSpeechResultById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Speech/Result/{{id}}", HandleAsync)
			.WithTags(Tags.Speech);
	}

	protected virtual async Task<Results<Ok<SpeechResult>, ProblemHttpResult, NotFound>> HandleAsync(int id, IResultData resultData)
	{
		try
		{
			var result = await resultData.GetSpeechAsync(id);
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
