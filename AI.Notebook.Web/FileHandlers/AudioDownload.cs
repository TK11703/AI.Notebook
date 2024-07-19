using AI.Notebook.Common.Entities;
using AI.Notebook.Web.Clients;
using AI.Notebook.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.Web.FileHandlers;

public class AudioDownload : IFileHandler
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Downloads/Audio/{{resultsId}}", HandleAsync).WithTags("Files");
	}

	protected virtual async Task<IResult> HandleAsync(int resultsId, ResultsClient resultsClient)
	{
		try
		{
			var resultItem = await resultsClient.Get(resultsId);

			return Results.File(resultItem.ResultData, "audio/wav", "audioDownload.wav");
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}
}
