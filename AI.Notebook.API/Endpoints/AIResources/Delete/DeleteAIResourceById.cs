
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.AIResources.Delete;

public class DeleteAIResourceById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapDelete($"/AIResources/{{id}}", HandleAsync)
			.WithTags(Tags.AIResources);
	}

	protected virtual async Task<Results<Ok<bool>, ProblemHttpResult>> HandleAsync(int id, IAIResourceData aIResourceData)
	{
		try
		{
			var completed = await aIResourceData.DeleteAsync(id);
			return TypedResults.Ok(completed);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
