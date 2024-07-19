using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Language.GetRequestById;

public class GetLanguageRequestById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Language/Request/{{id}}", HandleAsync)
			.WithTags(Tags.Language);
	}

	protected virtual async Task<Results<Ok<LanguageRequest>, ProblemHttpResult, NotFound>> HandleAsync(int id, IRequestData requestData)
	{
		try
		{
			var result = await requestData.GetLanguageAsync(id);
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
