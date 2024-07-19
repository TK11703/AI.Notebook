using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.AIRequests.GetAll;

public class GetAllAIRequests : IEndpoint
{	
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Requests", HandleAsync)
			.WithTags(Tags.AIRequests); //opting for all results in one screen.
	}

	protected virtual async Task<Results<Ok<IEnumerable<RequestBase>>, ProblemHttpResult>> HandleAsync(IRequestData requestData)
	{
		try
		{
			return TypedResults.Ok(await requestData.GetAllAsync());
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
