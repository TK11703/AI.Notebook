using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.ResultTypes.Update;

public class UpdateResultType : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPut($"/ResultTypes/{{id}}", Handle)
			.AddEndpointFilter<ValidatorFilter<ResultType>>()
			.WithTags(Tags.ResultTypes);
	}

	protected virtual Results<Ok<int>, ProblemHttpResult> Handle(int id, ResultType aResultTypeModel, IResultTypeData resultTypeData)
	{
		try
		{
			var rowsUpdated = resultTypeData.Update(aResultTypeModel);
			return TypedResults.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
