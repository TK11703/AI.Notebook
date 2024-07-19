using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.AIRequests.Update;

public class UpdateAIRequest : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPut($"/Requests/{{id}}", Handle)
			.AddEndpointFilter<ValidatorFilter<RequestBase>>()
			.WithTags(Tags.AIRequests);
	}

	protected virtual Results<Ok<int>, ProblemHttpResult> Handle(int id, RequestBase aRequestModel, IRequestData requestData)
	{
		try
		{
			var rowsUpdated = requestData.Update(aRequestModel);
			return TypedResults.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
