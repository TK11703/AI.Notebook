
using AI.Notebook.API.Filters;
using Microsoft.Extensions.Logging;

namespace AI.Notebook.API.Routers;

public class ResultTypeRouter : RouterBase
{
	public ResultTypeRouter(ILogger<ResultTypeRouter> logger)
	{
		UrlFragment = "ResultTypes";
		Logger = logger;
	}

	public override void AddRoutes(WebApplication app)
	{
		app.MapGet($"/{UrlFragment}", GetAllAsync);
		app.MapGet($"/{UrlFragment}/{{id}}", GetAsync);
		app.MapPost($"/{UrlFragment}", InsertAsync).AddEndpointFilter<ValidatorFilter<ResultTypeModel>>();
		app.MapPut($"/{UrlFragment}/{{id}}", Update).AddEndpointFilter<ValidatorFilter<ResultTypeModel>>();
		app.MapDelete($"/{UrlFragment}", DeleteAsync);
	}

	protected virtual async Task<IResult> GetAllAsync(IResultTypeData resultTypeData)
	{
		try
		{
			return Results.Ok(await resultTypeData.GetAllAsync());
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> GetAsync(int id, IResultTypeData resultTypeData)
	{
		try
		{
			var result = await resultTypeData.GetAsync(id);
			if (result == null)
				return Results.NotFound();
			return Results.Ok(result);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> InsertAsync(ResultTypeModel aResultTypeModel, IResultTypeData resultTypeData)
	{
		try
		{
			var newId = await resultTypeData.InsertAsync(aResultTypeModel);
			return Results.Ok(newId);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual IResult Update(int id, ResultTypeModel aResultTypeModel, IResultTypeData resultTypeData)
	{
		try
		{
			var rowsUpdated = resultTypeData.Update(aResultTypeModel);
			return Results.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> DeleteAsync(int id, IResultTypeData resultTypeData)
	{
		try
		{
			var completed = await resultTypeData.DeleteAsync(id);
			return Results.Ok(completed);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}
}
