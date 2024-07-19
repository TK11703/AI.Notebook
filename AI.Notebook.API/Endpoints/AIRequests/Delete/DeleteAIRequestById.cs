using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.AIRequests.Delete;

public class DeleteAIRequestById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapDelete($"/Requests/{{id}}", HandleAsync)
			.WithTags(Tags.AIRequests);
	}

	protected virtual async Task<Results<Ok<bool>, ProblemHttpResult>> HandleAsync(int id, IRequestData requestData)
	{
		try
		{
			var completed = await requestData.DeleteAsync(id);
			return TypedResults.Ok(completed);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
