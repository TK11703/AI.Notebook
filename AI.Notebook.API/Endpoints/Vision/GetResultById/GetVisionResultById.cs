using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Vision.GetResultById;

public class GetVisionResultById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Vision/Result/{{id}}", HandleAsync)
			.WithTags(Tags.Vision);
	}

	protected virtual async Task<Results<Ok<VisionResult>, ProblemHttpResult, NotFound>> HandleAsync(int id, IResultData resultData)
	{
		try
		{
			var result = await resultData.GetVisionAsync(id);
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
