using AI.Notebook.API.Filters;
using AI.Notebook.API.Processors;
using AI.Notebook.Common.AI.Text;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Translator.Transliterate;

public class ExecuteTransliterateRequest : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost($"/Translator/Transliterate", HandleAsync)
			.AddEndpointFilter<ValidatorFilter<TranslatorRequest>>()
			.WithTags(Tags.Translator);
	}

	protected virtual async Task<Results<Ok<TranslatorResult>, ProblemHttpResult>> HandleAsync(TranslatorRequest aRequestModel, IResultData resultData, IResultTypeData resultTypeData, IConfiguration config)
	{
		TextTransliterationResult result = null;
		try
		{
			TextTranslationServices services = new TextTranslationServices(config.GetValue<string>("Azure:SubscriptionKey"), config.GetValue<string>("AzureAI:Translator:Key"), config.GetValue<string>("AzureAI:Translator:Region"), config.GetValue<string>("AzureAI:Speech:Key"));
			result = await services.Transliterate(aRequestModel.SourceLangCode, aRequestModel.SourceScriptCode, aRequestModel.TargetScriptCode, aRequestModel.Input.Split(Environment.NewLine));
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
		if (result == null)
		{
			return TypedResults.Problem("Transliteration failed");
		}
		try
		{
			ResultProcessor processor = new ResultProcessor();
			TranslatorResult? modelResult = await processor.SaveTransliterationResultAsync(result, aRequestModel, resultData, resultTypeData);
			if (modelResult == null)
			{
				return TypedResults.Problem("Failed to save result");
			}
			return TypedResults.Ok(modelResult);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
