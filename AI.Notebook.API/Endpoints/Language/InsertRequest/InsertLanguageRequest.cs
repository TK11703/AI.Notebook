using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Language.InsertRequest;

public class InsertLanguageRequest : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost($"/Language/Request", HandleAsync)
			.AddEndpointFilter<ValidatorFilter<LanguageRequest>>()
			.WithTags(Tags.Language);
	}

	protected virtual async Task<Results<Ok<int>, ProblemHttpResult>> HandleAsync(LanguageRequest aRequestModel, IRequestData requestData)
	{
		try
		{
			int newId = await requestData.InsertLanguageAsync(aRequestModel);
			return TypedResults.Ok(newId);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
