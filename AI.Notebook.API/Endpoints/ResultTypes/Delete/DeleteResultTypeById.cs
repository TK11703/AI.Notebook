using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.ResultTypes.Delete;

public class DeleteResultTypeById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapDelete($"/ResultTypes/{{id}}", HandleAsync)
			.WithTags(Tags.ResultTypes);
	}

	protected virtual async Task<Results<Ok<bool>, ProblemHttpResult>> HandleAsync(int id, IResultTypeData resultTypeData)
	{
		try
		{
			var completed = await resultTypeData.DeleteAsync(id);
			return TypedResults.Ok(completed);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
