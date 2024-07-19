using Microsoft.AspNetCore.Http.HttpResults;
namespace AI.Notebook.API.Endpoints.Vision.GetRequestById;

public class GetVisionRequestById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Vision/Request/{{id}}", HandleAsync)
			.WithTags(Tags.Vision);
	}

	protected virtual async Task<Results<Ok<VisionRequest>, ProblemHttpResult, NotFound>> HandleAsync(int id, IRequestData requestData)
	{
		try
		{
			var result = await requestData.GetVisionAsync(id);
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
