
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Translator.GetRequestById;

public class GetTranslatorRequestById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Translator/Request/{{id}}", HandleAsync)
			.WithTags(Tags.Translator);
	}

	protected virtual async Task<Results<Ok<TranslatorRequest>, ProblemHttpResult, NotFound>> HandleAsync(int id, IRequestData requestData)
	{
		try
		{
			var result = await requestData.GetTranslatorAsync(id);
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
