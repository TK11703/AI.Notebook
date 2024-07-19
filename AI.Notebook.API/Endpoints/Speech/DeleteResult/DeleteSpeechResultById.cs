
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Speech.DeleteResult;

public class DeleteSpeechResultById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapDelete($"/Speech/Result/{{id}}", HandleAsync)
			.WithTags(Tags.Speech);
	}

	protected virtual async Task<Results<Ok<bool>, ProblemHttpResult>> HandleAsync(int id, IResultData resultData)
	{
		try
		{
			var completed = await resultData.DeleteSpeechResultAsync(id);
			return TypedResults.Ok(completed);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
