
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.AIResources.GetAll;

public class GetAllAIResources : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/AIResources", HandleAsync)
			.WithTags(Tags.AIResources);
	}

	protected virtual async Task<Results<Ok<IEnumerable<AIResource>>, ProblemHttpResult>> HandleAsync(IAIResourceData aIResourceData)
	{
		try
		{
			return TypedResults.Ok(await aIResourceData.GetAllAsync());
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
