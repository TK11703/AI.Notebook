
using AI.Notebook.API.Filters;
using AI.Notebook.API.Processors;
using AI.Notebook.Common.AI.Image;
using AI.Notebook.Common.AI.Speech;
using AI.Notebook.Common.AI.Text;
using AI.Notebook.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AI.Notebook.API.Routers;

public class ServicesRouter : RouterBase
{
	private IConfiguration _config { get; set; }
	public ServicesRouter(ILogger<ServicesRouter> logger, IConfiguration configuration)
	{
		UrlFragment = "Services";
		Logger = logger;
		_config = configuration;
	}

	public override void AddRoutes(WebApplication app)
	{
		app.MapGet($"/{UrlFragment}/Translator/GetLangauges", GetTranslatorLanguagesAsync);
		app.MapPost($"/{UrlFragment}/Translator/Translate", ExecuteTranslatorAsync).AddEndpointFilter<ValidatorFilter<RequestTranslatorModel>>();
		//app.MapPost($"/{UrlFragment}/Translator/Transliterate", ExecuteTransliteratorAsync).AddEndpointFilter<ValidatorFilter<RequestTranslatorModel>>();
		//app.MapPost($"/{UrlFragment}/Speech", ExecuteSpeechAsync).AddEndpointFilter<ValidatorFilter<RequestSpeechModel>>();
		//app.MapPost($"/{UrlFragment}/Vision", ExecuteVisionAsync).AddEndpointFilter<ValidatorFilter<RequestVisionModel>>();
		//app.MapPost($"/{UrlFragment}/Language", ExecuteLanguageAsync).AddEndpointFilter<ValidatorFilter<RequestLanguageModel>>();
	}

	protected virtual async Task<IResult> GetTranslatorLanguagesAsync()
	{
		try
		{
			TextTranslationServices services = new TextTranslationServices(_config.GetValue<string>("AzureAI:Translator:Key"), _config.GetValue<string>("AzureAI:Translator:Region"));
			SupportedLanguagesResult langResult = await services.GetSupportedLanguages();
			return Results.Ok(langResult);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> ExecuteTranslatorAsync(RequestTranslatorModel aRequestModel, IResultData resultData, IResultTypeData resultTypeData)
	{
		TextTranslationResult result = null;
		try
		{
			TextTranslationServices services = new TextTranslationServices(_config.GetValue<string>("AzureAI:Translator:Key"), _config.GetValue<string>("AzureAI:Translator:Region"));
			result = await services.Translate(aRequestModel.SourceLangCode, aRequestModel.TargetLangCode, aRequestModel.Input, aRequestModel.OutputAsAudio, aRequestModel.VoiceName);
		}
		catch(Exception ex)
		{
			return Results.Problem(ex.Message);
		}
		if (result == null)
		{
			return Results.Problem("Translation failed");
		}
		try
		{
			ResultProcessor processor = new ResultProcessor();
			ResultTranslatorModel? modelResult = await processor.SaveTranslatorResultAsync(result, aRequestModel, resultData, resultTypeData);
			if (modelResult == null)
			{
				return Results.Problem("Failed to save translation result");
			}
			return Results.Ok(modelResult);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}	

	//protected virtual async Task<IResult> ExecuteSpeechAsync(RequestSpeechModel aRequestModel, IResultData resultData)
	//{
	//	try
	//	{
	//		SpeechServices services = new SpeechServices(_config.GetValue<string>("ServiceKey"), _config.GetValue<string>("ServiceRegion"));
	//		int newId = await requestData.InsertAsync(aRequestModel);
	//		if (newId > 0)
	//		{
	//			aRequestModel.RequestId = newId;
	//			await requestData.InsertSpeechAsync(aRequestModel);
	//		}
	//		return Results.Ok(newId);
	//	}
	//	catch (Exception ex)
	//	{
	//		return Results.Problem(ex.Message);
	//	}
	//}

	//protected virtual async Task<IResult> ExecuteVisionAsync(RequestVisionModel aRequestModel, IResultData resultData)
	//{
	//	try
	//	{
	//		ImageServices services = new ImageServices(_config.GetValue<string>("ServiceKey"), _config.GetValue<string>("ImageEndpoint"));
	//		int newId = await requestData.InsertAsync(aRequestModel);
	//		if (newId > 0)
	//		{
	//			aRequestModel.RequestId = newId;
	//			await requestData.InsertVisionAsync(aRequestModel);
	//		}
	//		return Results.Ok(newId);
	//	}
	//	catch (Exception ex)
	//	{
	//		return Results.Problem(ex.Message);
	//	}
	//}

	//protected virtual async Task<IResult> ExecuteLanguageAsync(RequestLanguageModel aRequestModel, IResultData resultData)
	//{
	//	try
	//	{
	//		TextTranslationServices services = new TextTranslationServices(_config.GetValue<string>("ServiceKey"), _config.GetValue<string>("ServiceRegion"));
	//		int newId = await requestData.InsertAsync(aRequestModel);
	//		if (newId > 0)
	//		{
	//			aRequestModel.RequestId = newId;
	//			await requestData.InsertLanguageAsync(aRequestModel);
	//		}
	//		return Results.Ok(newId);
	//	}
	//	catch (Exception ex)
	//	{
	//		return Results.Problem(ex.Message);
	//	}
	//}
}
