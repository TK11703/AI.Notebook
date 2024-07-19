using Microsoft.AspNetCore.Http.HttpResults;
namespace AI.Notebook.API.Endpoints.Translator.GetResultsByRequest;

public class GetTranslatorResultsByRequestId : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Translator/Results/Request/{{requestId}}", HandleAsync)
			.WithTags(Tags.Translator);
	}

	protected virtual async Task<Results<Ok<IEnumerable<ResultBase>>, ProblemHttpResult>> HandleAsync(int requestId, IResultData resultData)
	{
		try
		{
			return TypedResults.Ok(await resultData.GetTranslatorResultsByRequestAsync(requestId));
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
