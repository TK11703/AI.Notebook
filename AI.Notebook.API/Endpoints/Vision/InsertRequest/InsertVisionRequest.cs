using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Vision.InsertRequest;

public class InsertVisionRequest : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost($"/Vision/Request", HandleAsync)
			.AddEndpointFilter<ValidatorFilter<VisionRequest>>()
			.WithTags(Tags.Vision);
	}

	protected virtual async Task<Results<Ok<int>, ProblemHttpResult>> HandleAsync(VisionRequest aRequestModel, IRequestData requestData)
	{
		try
		{
			int newId = await requestData.InsertVisionAsync(aRequestModel);
			return TypedResults.Ok(newId);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
