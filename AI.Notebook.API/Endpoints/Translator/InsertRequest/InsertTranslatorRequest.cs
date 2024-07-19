using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Translator.InsertRequest;

public class InsertTranslatorRequest : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost($"/Translator/Request", HandleAsync)
			.AddEndpointFilter<ValidatorFilter<TranslatorRequest>>()
			.WithTags(Tags.Translator);
	}

	protected virtual async Task<Results<Ok<int>, ProblemHttpResult>> HandleAsync(TranslatorRequest aRequestModel, IRequestData requestData)
	{
		try
		{
			int newId = await requestData.InsertAsync(aRequestModel);
			if (newId > 0)
			{
				aRequestModel.RequestId = newId;
				await requestData.InsertTranslatorAsync(aRequestModel);
			}
			return TypedResults.Ok(newId);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
