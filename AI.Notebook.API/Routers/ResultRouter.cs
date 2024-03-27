
using AI.Notebook.API.Filters;
using Microsoft.Extensions.Logging;

namespace AI.Notebook.API.Routers;

public class ResultRouter : RouterBase
{
	public ResultRouter(ILogger<ResultRouter> logger)
	{
		UrlFragment = "Results";
		Logger = logger;
	}

	public override void AddRoutes(WebApplication app)
	{
		app.MapGet($"/{UrlFragment}/RequestId/{{requestId}}", GetByRequestAsync);
		app.MapGet($"/{UrlFragment}/ResourceId/{{resourceId}}", GetByResourceAsync);
		app.MapGet($"/{UrlFragment}/{{id}}", GetAsync);
		app.MapPost($"/{UrlFragment}", InsertAsync).AddEndpointFilter<ValidatorFilter<ResultModel>>();
		app.MapPut($"/{UrlFragment}/{{id}}", Update).AddEndpointFilter<ValidatorFilter<ResultModel>>();
		app.MapDelete($"/{UrlFragment}", DeleteAsync);
	}

	protected virtual async Task<IResult> GetByRequestAsync(int requestId, IResultData resultData)
	{
		try
		{
			return Results.Ok(await resultData.GetByRequestAsync(requestId));
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> GetByResourceAsync(int resourceId, IResultData resultData)
	{
		try
		{
			return Results.Ok(await resultData.GetByResourceAsync(resourceId));
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> GetAsync(int id, IResultData resultData)
	{
		try
		{
			var result = await resultData.GetAsync(id);
			if (result == null)
				return Results.NotFound();
			return Results.Ok(result);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> InsertAsync(ResultModel aResultModel, IResultData resultData)
	{
		try
		{
			int newId = await resultData.InsertAsync(aResultModel);
			return Results.Ok(newId);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual IResult Update(int id, ResultModel aResultModel, IResultData resultData)
	{
		try
		{
			var rowsUpdated = resultData.Update(aResultModel);
			return Results.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> DeleteAsync(int id, IResultData resultData)
	{
		try
		{
			var completed = await resultData.DeleteAsync(id);
			return Results.Ok(completed);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}
}
