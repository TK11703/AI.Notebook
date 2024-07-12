
using AI.Notebook.API.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AI.Notebook.API.Routers;

public class RequestRouter : RouterBase
{
	public RequestRouter(ILogger<RequestRouter> logger)
	{
		UrlFragment = "Requests";
		Logger = logger;
	}

	public override void AddRoutes(WebApplication app)
	{
		app.MapGet($"/{UrlFragment}", GetAllAsync); //opting for paged results over all results in one screen.
		app.MapGet($"/{UrlFragment}/PagedRequest", GetPagedAsync);
		app.MapGet($"/{UrlFragment}/{{id}}", GetAsync);

		//app.MapPost($"/{UrlFragment}", InsertAsync).AddEndpointFilter<ValidatorFilter<RequestModel>>();
		app.MapPut($"/{UrlFragment}/{{id}}", Update).AddEndpointFilter<ValidatorFilter<RequestModel>>();

		app.MapGet($"/{UrlFragment}/Translator/{{id}}", GetTranslatorAsync);
		app.MapPost($"/{UrlFragment}/Translator", InsertTranslatorAsync).AddEndpointFilter<ValidatorFilter<RequestTranslatorModel>>();
		app.MapPut($"/{UrlFragment}/Translator/{{id}}", UpdateTranslator).AddEndpointFilter<ValidatorFilter<RequestTranslatorModel>>();

		app.MapGet($"/{UrlFragment}/Speech/{{id}}", GetSpeechAsync);
		app.MapPost($"/{UrlFragment}/Speech", InsertSpeechAsync).AddEndpointFilter<ValidatorFilter<RequestSpeechModel>>();
		app.MapPut($"/{UrlFragment}/Speech/{{id}}", UpdateSpeech).AddEndpointFilter<ValidatorFilter<RequestSpeechModel>>();

		app.MapGet($"/{UrlFragment}/Vision/{{id}}", GetVisionAsync);
		app.MapPost($"/{UrlFragment}/Vision", InsertVisionAsync).AddEndpointFilter<ValidatorFilter<RequestVisionModel>>();
		app.MapPut($"/{UrlFragment}/Vision/{{id}}", UpdateVision).AddEndpointFilter<ValidatorFilter<RequestVisionModel>>();

		app.MapGet($"/{UrlFragment}/Language/{{id}}", GetLanguageAsync);
		app.MapPost($"/{UrlFragment}/Language", InsertLanguageAsync).AddEndpointFilter<ValidatorFilter<RequestLanguageModel>>();
		app.MapPut($"/{UrlFragment}/Language/{{id}}", UpdateLanguage).AddEndpointFilter<ValidatorFilter<RequestLanguageModel>>();

		app.MapDelete($"/{UrlFragment}", DeleteAsync);
	}

	protected virtual async Task<IResult> GetAllAsync(IRequestData requestData)
	{
		try
		{
			return Results.Ok(await requestData.GetAllAsync());
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> GetPagedAsync([FromBody] PageSubmissionModel pageRequest, IRequestData requestData)
	{
		try
		{
			return Results.Ok(await requestData.GetPagedAsync(pageRequest));
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> GetAsync(int id, IRequestData requestData)
	{
		try
		{
			var result = await requestData.GetAsync(id);
			if (result == null)
				return Results.NotFound();
			return Results.Ok(result);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> GetTranslatorAsync(int id, IRequestData requestData)
	{
		try
		{
			var result = await requestData.GetTranslatorAsync(id);
			if (result == null)
				return Results.NotFound();
			return Results.Ok(result);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> GetSpeechAsync(int id, IRequestData requestData)
	{
		try
		{
			var result = await requestData.GetSpeechAsync(id);
			if (result == null)
				return Results.NotFound();
			return Results.Ok(result);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> GetVisionAsync(int id, IRequestData requestData)
	{
		try
		{
			var result = await requestData.GetVisionAsync(id);
			if (result == null)
				return Results.NotFound();
			return Results.Ok(result);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> GetLanguageAsync(int id, IRequestData requestData)
	{
		try
		{
			var result = await requestData.GetLanguageAsync(id);
			if (result == null)
				return Results.NotFound();
			return Results.Ok(result);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	//protected virtual async Task<IResult> InsertAsync(RequestModel aRequestModel, IRequestData requestData)
	//{
	//	try
	//	{
	//		int newId = await requestData.InsertAsync(aRequestModel);
	//		return Results.Ok(newId);
	//	}
	//	catch (Exception ex)
	//	{
	//		return Results.Problem(ex.Message);
	//	}
	//}

	protected virtual async Task<IResult> InsertTranslatorAsync(RequestTranslatorModel aRequestModel, IRequestData requestData)
	{
		try
		{
			int newId = await requestData.InsertAsync(aRequestModel);
			if (newId > 0)
			{
				aRequestModel.RequestId = newId;
				await requestData.InsertTranslatorAsync(aRequestModel);
			}
			return Results.Ok(newId);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> InsertSpeechAsync(RequestSpeechModel aRequestModel, IRequestData requestData)
	{
		try
		{
			int newId = await requestData.InsertAsync(aRequestModel);
			if (newId > 0)
			{
				aRequestModel.RequestId = newId;
				await requestData.InsertSpeechAsync(aRequestModel);
			}
			return Results.Ok(newId);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> InsertVisionAsync(RequestVisionModel aRequestModel, IRequestData requestData)
	{
		try
		{
			int newId = await requestData.InsertAsync(aRequestModel);
			if (newId > 0)
			{
				aRequestModel.RequestId = newId;
				await requestData.InsertVisionAsync(aRequestModel);
			}
			return Results.Ok(newId);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> InsertLanguageAsync(RequestLanguageModel aRequestModel, IRequestData requestData)
	{
		try
		{
			int newId = await requestData.InsertAsync(aRequestModel);
			if (newId > 0)
			{
				aRequestModel.RequestId = newId;
				await requestData.InsertLanguageAsync(aRequestModel);
			}
			return Results.Ok(newId);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual IResult Update(int id, RequestModel aRequestModel, IRequestData requestData)
	{
		try
		{
			var rowsUpdated = requestData.Update(aRequestModel);
			return Results.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual IResult UpdateTranslator(int id, RequestTranslatorModel aRequestModel, IRequestData requestData)
	{
		try
		{
			var rowsUpdated = requestData.UpdateTranslator(aRequestModel);
			return Results.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual IResult UpdateSpeech(int id, RequestSpeechModel aRequestModel, IRequestData requestData)
	{
		try
		{
			var rowsUpdated = requestData.UpdateSpeech(aRequestModel);
			return Results.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual IResult UpdateVision(int id, RequestVisionModel aRequestModel, IRequestData requestData)
	{
		try
		{
			var rowsUpdated = requestData.UpdateVision(aRequestModel);
			return Results.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual IResult UpdateLanguage(int id, RequestLanguageModel aRequestModel, IRequestData requestData)
	{
		try
		{
			var rowsUpdated = requestData.UpdateLanguage(aRequestModel);
			return Results.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> DeleteAsync(int id, IRequestData requestData)
	{
		try
		{
			var completed = await requestData.DeleteAsync(id);
			return Results.Ok(completed);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}
}
