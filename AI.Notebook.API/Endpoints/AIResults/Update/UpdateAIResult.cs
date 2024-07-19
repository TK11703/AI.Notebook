using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.AITypedResults.Update;

public class UpdateAIResult : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPut($"/Results/{{id}}", Handle)
			.AddEndpointFilter<ValidatorFilter<ResultBase>>()
			.WithTags(Tags.AIResults);
	}

	protected virtual Results<Ok<int>, ProblemHttpResult> Handle(int id, ResultBase aResultModel, IResultData resultData)
	{
		try
		{
			var rowsUpdated = resultData.Update(aResultModel);
			return TypedResults.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
