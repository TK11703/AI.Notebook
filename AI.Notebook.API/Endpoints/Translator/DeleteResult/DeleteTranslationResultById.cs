
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Translator.DeleteResult;

public class DeleteTranslationResultById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapDelete($"/Translator/Result/{{id}}/{{resultId}}", HandleAsync)
			.WithTags(Tags.AIResults);
	}

	protected virtual async Task<Results<Ok<bool>, ProblemHttpResult>> HandleAsync(int id, int resultId, IResultData resultData)
	{
		try
		{
			var completed = await resultData.DeleteTranslatorResultAsync(id, resultId);
			return TypedResults.Ok(completed);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
