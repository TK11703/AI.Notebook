using AI.Notebook.API.Filters;
using AI.Notebook.API.Processors;
using AI.Notebook.Common.AI.Text;
using AI.Notebook.Common.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AI.Notebook.API.Endpoints.Translator.Translate;

public class ExecuteTranslationRequest: IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost($"/Translator/Translate", HandleAsync)
			.AddEndpointFilter<ValidatorFilter<TranslatorRequest>>()
			.WithTags(Tags.Translator);
	}

	protected virtual async Task<Results<Ok<TranslatorResult>, ProblemHttpResult>> HandleAsync(TranslatorRequest aRequestModel, IResultData resultData, IResultTypeData resultTypeData, IConfiguration config)
	{
		TextTranslationResult result = null;
		try
		{
			TextTranslationServices services = new TextTranslationServices(config.GetValue<string>("Azure:SubscriptionKey"), config.GetValue<string>("AzureAI:Translator:Key"), config.GetValue<string>("AzureAI:Translator:Region"), config.GetValue<string>("AzureAI:Speech:Key"));
			result = await services.Translate(aRequestModel.SourceLangCode, aRequestModel.TargetLangCode, aRequestModel.Input, aRequestModel.OutputAsAudio, aRequestModel.VoiceName);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
		if (result == null)
		{
			return TypedResults.Problem("Translation failed");
		}
		try
		{
			ResultProcessor processor = new ResultProcessor();
			TranslatorResult? modelResult = await processor.SaveTranslatorResultAsync(result, aRequestModel, resultData, resultTypeData);
			if (modelResult == null)
			{
				return TypedResults.Problem("Failed to save translation result");
			}
			return TypedResults.Ok(modelResult);
		}
		catch (Exception ex)
		{
			return TypedResults.Problem(ex.Message);
		}
	}
}
