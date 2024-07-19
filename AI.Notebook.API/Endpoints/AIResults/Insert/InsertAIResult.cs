using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.AITypedResults.Insert;

public class InsertAIResult : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost($"/Results", HandleAsync)
			.AddEndpointFilter<ValidatorFilter<ResultBase>>()
			.WithTags(Tags.AIResults);
	}
	protected virtual async Task<Results<Ok<int>, ProblemHttpResult>> HandleAsync(ResultBase aResultModel, IResultData resultData)
	{
		try
		{
			int newId = await resultData.InsertAsync(aResultModel);
			return TypedResults.Ok(newId);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
