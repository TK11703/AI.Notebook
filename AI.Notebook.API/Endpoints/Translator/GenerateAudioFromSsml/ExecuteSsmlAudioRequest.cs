using AI.Notebook.API.Filters;
using AI.Notebook.API.Processors;
using AI.Notebook.Common.AI.Text;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Translator.GenerateAudioFromSsml;

public class ExecuteSsmlAudioRequest : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost($"/Translator/GenerateSsmlAudio", HandleAsync)
			.WithTags(Tags.Translator);
	}

	protected virtual async Task<Results<Ok<byte[]>, ProblemHttpResult>> HandleAsync(SsmlRequest request, IConfiguration config)
	{
		try
		{
			TextTranslationServices services = new TextTranslationServices(config.GetValue<string>("Azure:SubscriptionKey"), config.GetValue<string>("AzureAI:Translator:Key"), config.GetValue<string>("AzureAI:Translator:Region"), config.GetValue<string>("AzureAI:Speech:Key"));
			return TypedResults.Ok(await services.GenerateSsmlAudio(request.Ssml));
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
