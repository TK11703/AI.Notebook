
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
		app.MapPost($"/{UrlFragment}", InsertAsync).AddEndpointFilter<ValidatorFilter<RequestModel>>();
		app.MapPut($"/{UrlFragment}/{{id}}", Update).AddEndpointFilter<ValidatorFilter<RequestModel>>();
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

	protected virtual async Task<IResult> InsertAsync(RequestModel aRequestModel, IRequestData requestData)
	{
		try
		{
			int newId = await requestData.InsertAsync(aRequestModel);
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
