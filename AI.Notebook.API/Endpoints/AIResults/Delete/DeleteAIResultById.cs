
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.AITypedResults.Delete;

public class DeleteAIResultById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapDelete($"/Results/{{id}}", HandleAsync)
			.WithTags(Tags.AIResults);
	}

	protected virtual async Task<Results<Ok<bool>, ProblemHttpResult>> HandleAsync(int id, IResultData resultData)
	{
		try
		{
			var completed = await resultData.DeleteAsync(id);
			return TypedResults.Ok(completed);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
