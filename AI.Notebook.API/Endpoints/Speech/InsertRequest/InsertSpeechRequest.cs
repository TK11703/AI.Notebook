using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Speech.InsertRequest;

public class InsertSpeechRequest : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost($"/Speech/Request", HandleAsync)
			.AddEndpointFilter<ValidatorFilter<SpeechRequest>>()
			.WithTags(Tags.Speech);
	}

	protected virtual async Task<Results<Ok<int>, ProblemHttpResult>> HandleAsync(SpeechRequest aRequestModel, IRequestData requestData)
	{
		try
		{
			int newId = await requestData.InsertSpeechAsync(aRequestModel);
			return TypedResults.Ok(newId);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
