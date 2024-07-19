using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.AITypedResults.GetByAIResource;

public class GetAIResultByAIResourceId : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Results/AIResource/{{resourceId}}", HandleAsync)
			.WithTags(Tags.AIResults);
	}

	protected virtual async Task<Results<Ok<IEnumerable<ResultBase>>, ProblemHttpResult>> HandleAsync(int resourceId, IResultData resultData)
	{
		try
		{
			return TypedResults.Ok(await resultData.GetByResourceAsync(resourceId));
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
