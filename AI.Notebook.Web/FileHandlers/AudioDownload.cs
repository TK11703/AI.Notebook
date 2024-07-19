using AI.Notebook.Web.Clients;
using AI.Notebook.Web.Extensions;

namespace AI.Notebook.Web.FileHandlers;

public class AudioDownload : IFileHandler
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Downloads/Audio/Translator/{{id}}", HandleTranslatorAsync).WithTags("Files");
	}

	protected virtual async Task<IResult> HandleTranslatorAsync(int id, TranslatorClient client)
	{
		try
		{
			var resultItem = await client.GetTranslationResult(id);

			return Results.File(resultItem.ResultAudio, "audio/wav", "audioDownload.wav");
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}
}
