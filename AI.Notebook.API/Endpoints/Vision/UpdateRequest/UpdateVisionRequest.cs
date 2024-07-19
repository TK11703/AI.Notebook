using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Vision.UpdateRequest;

public class UpdateVisionRequest : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPut($"/Vision/Request/{{id}}", Handle)
			.AddEndpointFilter<ValidatorFilter<VisionRequest>>()
			.WithTags(Tags.Vision);
	}

	protected virtual Results<Ok<int>, ProblemHttpResult> Handle(int id, VisionRequest aRequestModel, IRequestData requestData, ILogger<UpdateVisionRequest> logger)
	{
		try
		{
			var rowsUpdated = requestData.UpdateVision(aRequestModel);
			return TypedResults.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
