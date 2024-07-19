using AI.Notebook.API.Filters;
using AI.Notebook.Common.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Language.UpdateRequest;

public class UpdateLanguageRequest : IEndpoint
{	
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPut($"/Language/Request/{{id}}", Handle)
			.AddEndpointFilter<ValidatorFilter<LanguageRequest>>()
			.WithTags(Tags.Language);
	}

	protected virtual Results<Ok<int>, ProblemHttpResult> Handle(int id, LanguageRequest aRequestModel, IRequestData requestData)
	{
		try
		{
			var rowsUpdated = requestData.UpdateLanguage(aRequestModel);
			return TypedResults.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
