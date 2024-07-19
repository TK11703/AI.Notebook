using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.AIResources.GetById;

public class GetAIResourceById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/AIResources/{{id}}", HandleAsync)
			.WithTags(Tags.AIResources);
	}

	protected virtual async Task<Results<Ok<AIResource>, ProblemHttpResult, NotFound>> HandleAsync(int id, IAIResourceData aIResourceData)
	{
		try
		{
			var result = await aIResourceData.GetAsync(id);
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
