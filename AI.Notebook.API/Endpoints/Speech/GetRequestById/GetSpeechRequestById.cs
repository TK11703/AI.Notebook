
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Speech.GetRequestById;

public class GetSpeechRequestById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Speech/Request/{{id}}", HandleAsync)
			.WithTags(Tags.Speech);
	}

	protected virtual async Task<Results<Ok<SpeechRequest>, ProblemHttpResult, NotFound>> HandleAsync(int id, IRequestData requestData)
	{
		try
		{
			var result = await requestData.GetSpeechAsync(id);
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
