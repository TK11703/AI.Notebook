using AI.Notebook.Common.AI.Text;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Translator.GetLanguages;

public class GetAvailableLanguages : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet($"/Translator/Languages", HandleAsync)
			.WithTags(Tags.Translator);
	}

	protected virtual async Task<Results<Ok<SupportedLanguagesResult>, ProblemHttpResult>> HandleAsync(IResultData resultData, IConfiguration config)
	{
		try
		{
			TextTranslationServices services = new TextTranslationServices(config.GetValue<string>("Azure:SubscriptionKey"), config.GetValue<string>("AzureAI:Translator:Key"), config.GetValue<string>("AzureAI:Translator:Region"), config.GetValue<string>("AzureAI:Speech:Key"));
			SupportedLanguagesResult langResult = await services.GetSupportedLanguages();
			return TypedResults.Ok(langResult);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
