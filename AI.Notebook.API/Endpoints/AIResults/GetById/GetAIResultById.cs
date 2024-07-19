using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.AITypedResults.GetById;

public class GetAIResultById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Results/{{id}}", HandleAsync)
			.WithTags(Tags.AIResults);
	}

	protected virtual async Task<Results<Ok<ResultBase>, ProblemHttpResult, NotFound>> HandleAsync(int id, IResultData resultData)
	{
		try
		{
			var result = await resultData.GetAsync(id);
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
