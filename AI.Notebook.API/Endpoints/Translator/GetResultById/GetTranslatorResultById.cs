using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Translator.GetResultById;

public class GetTranslatorResultById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Translator/Result/{{id}}", HandleAsync)
			.WithTags(Tags.Translator);
	}

	protected virtual async Task<Results<Ok<TranslatorResult>, ProblemHttpResult, NotFound>> HandleAsync(int id, IResultData resultData)
	{
		try
		{
			var result = await resultData.GetTranslatorAsync(id);
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
