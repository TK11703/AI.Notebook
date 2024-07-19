using AI.Notebook.Common.Entities;
using AI.Notebook.Web.Clients;
using AI.Notebook.Web.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AI.Notebook.Web.FileHandlers;

public class VideoDownload : IFileHandler
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Downloads/Video/{{resultsId}}", HandleAsync).WithTags("Files");
	}

	protected virtual async Task<IResult> HandleAsync(int resultsId, ResultsClient resultsClient)
	{
		try
		{
			var resultItem = await resultsClient.Get(resultsId);

			return Results.File(resultItem.ResultData, "application/octet-stream", "videoDownload.mpg", true);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}
}
