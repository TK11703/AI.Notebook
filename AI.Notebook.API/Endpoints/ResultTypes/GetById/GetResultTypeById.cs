using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.ResultTypes.GetById;

public class GetResultTypeById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/ResultTypes/{{id}}", HandleAsync)
			.WithTags(Tags.ResultTypes);
	}

	protected virtual async Task<Results<Ok<ResultType>, ProblemHttpResult, NotFound>> HandleAsync(int id, IResultTypeData resultTypeData)
	{
		try
		{
			var result = await resultTypeData.GetAsync(id);
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
