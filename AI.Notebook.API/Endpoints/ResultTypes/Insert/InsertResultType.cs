using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.ResultTypes.Insert;

public class InsertResultType : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost($"/ResultTypes", HandleAsync)
			.AddEndpointFilter<ValidatorFilter<ResultType>>()
			.WithTags(Tags.ResultTypes);
	}

	protected virtual async Task<Results<Ok<int>, ProblemHttpResult>> HandleAsync(ResultType aResultTypeModel, IResultTypeData resultTypeData)
	{
		try
		{
			var newId = await resultTypeData.InsertAsync(aResultTypeModel);
			return TypedResults.Ok(newId);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}