using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.ResultTypes.GetAll;

public class GetAllResultTypes : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/ResultTypes", HandleAsync)
			.WithTags(Tags.ResultTypes);
	}

	protected virtual async Task<Results<Ok<IEnumerable<ResultType>>, ProblemHttpResult>> HandleAsync(IResultTypeData resultTypeData)
	{
		try
		{
			return TypedResults.Ok(await resultTypeData.GetAllAsync());
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
