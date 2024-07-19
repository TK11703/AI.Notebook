using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Language.GetResultById;

public class GetLanguageResultById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Language/Result/{{id}}", HandleAsync)
			.WithTags(Tags.Language);
	}

	protected virtual async Task<Results<Ok<LanguageResult>, ProblemHttpResult, NotFound>> HandleAsync(int id, IResultData resultData)
	{
		try
		{
			var result = await resultData.GetLanguageAsync(id);
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
