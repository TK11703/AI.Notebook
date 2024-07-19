using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.AIResources.Insert;

public class InsertNewAIResource : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost($"/AIResources", HandleAsync)
			.AddEndpointFilter<ValidatorFilter<AIResource>>()
			.WithTags(Tags.AIResources);
	}

	protected virtual async Task<Results<Ok<int>, ProblemHttpResult>> HandleAsync(AIResource aIResourceModel, IAIResourceData aIResourceData)
	{
		try
		{
			int newId = await aIResourceData.InsertAsync(aIResourceModel);
			return TypedResults.Ok(newId);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
