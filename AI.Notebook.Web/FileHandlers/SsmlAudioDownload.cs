using AI.Notebook.Common.Entities;
using AI.Notebook.Web.Clients;
using AI.Notebook.Web.Extensions;
using Azure.Core;

namespace AI.Notebook.Web.FileHandlers;

public class SsmlAudioDownload : IFileHandler
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost($"/Downloads/Audio/Translator/Ssml", HandleTranslatorAsync).WithTags("Files");
	}

	protected virtual async Task<IResult> HandleTranslatorAsync(SsmlRequest request, TranslatorClient client)
	{
		try
		{
			var resultItem = await client.GenerateSsml(request);

			return Results.File(resultItem, "audio/wav", "audioDownload.wav");
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}
}
