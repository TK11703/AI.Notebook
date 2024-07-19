using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Translator.UpdateRequest;

public class UpdateTranslatorRequest : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPut($"/Translator/Request/{{id}}", Handle)
			.AddEndpointFilter<ValidatorFilter<TranslatorRequest>>()
			.WithTags(Tags.Translator);
	}

	protected virtual Results<Ok<int>, ProblemHttpResult> Handle(int id, TranslatorRequest aRequestModel, IRequestData requestData)
	{
		try
		{
			var rowsUpdated = requestData.UpdateTranslator(aRequestModel);
			return TypedResults.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
