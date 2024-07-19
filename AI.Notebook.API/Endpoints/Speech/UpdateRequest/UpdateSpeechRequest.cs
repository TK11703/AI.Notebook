using AI.Notebook.API.Filters;
using AI.Notebook.Common.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Speech.UpdateRequest;

public class UpdateSpeechRequest : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPut($"/Speech/Request/{{id}}", Handle)
			.AddEndpointFilter<ValidatorFilter<SpeechRequest>>()
			.WithTags(Tags.Speech);
	}

	protected virtual Results<Ok<int>, ProblemHttpResult> Handle(int id, SpeechRequest aRequestModel, IRequestData requestData)
	{
		try
		{
			var rowsUpdated = requestData.UpdateSpeech(aRequestModel);
			return TypedResults.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
