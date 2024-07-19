using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.AIResources.Update;

public class UpdateAIResource : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPut($"/AIResources/{{id}}", Handle)
			.AddEndpointFilter<ValidatorFilter<AIResource>>()
			.WithTags(Tags.AIResources);
	}

	protected virtual Results<Ok<int>, ProblemHttpResult> Handle(int id, AIResource aIResourceModel, IAIResourceData aIResourceData)
	{
		try
		{
			var rowsUpdated = aIResourceData.Update(aIResourceModel);
			return TypedResults.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
