
using AI.Notebook.API.Filters;

namespace AI.Notebook.API.Routers;

public class AIResourceRouter : RouterBase
{
	public AIResourceRouter(ILogger<AIResourceRouter> logger)
	{
		UrlFragment = "AIResources";
		Logger = logger;
	}

	public override void AddRoutes(WebApplication app)
	{
		app.MapGet($"/{UrlFragment}", GetAllAsync);
		app.MapGet($"/{UrlFragment}/{{id}}", GetAsync);
		app.MapPost($"/{UrlFragment}", InsertAsync).AddEndpointFilter<ValidatorFilter<AIResourceModel>>();
		app.MapPut($"/{UrlFragment}/{{id}}", Update).AddEndpointFilter<ValidatorFilter<AIResourceModel>>();
		app.MapDelete($"/{UrlFragment}", DeleteAsync);
	}

	protected virtual async Task<IResult> GetAllAsync(IAIResourceData aIResourceData)
	{
		try
		{
			return Results.Ok(await aIResourceData.GetAllAsync());
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> GetAsync(int id, IAIResourceData aIResourceData)
	{
		try
		{
			var result = await aIResourceData.GetAsync(id);
			if (result == null)
				return Results.NotFound();
			return Results.Ok(result);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> InsertAsync(AIResourceModel aIResourceModel, IAIResourceData aIResourceData)
	{
		try
		{
			int newId = await aIResourceData.InsertAsync(aIResourceModel);
			return Results.Ok(newId);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual IResult Update(int id, AIResourceModel aIResourceModel, IAIResourceData aIResourceData)
	{
		try
		{
			var rowsUpdated = aIResourceData.Update(aIResourceModel);
			return Results.Ok(rowsUpdated);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}

	protected virtual async Task<IResult> DeleteAsync(int id, IAIResourceData aIResourceData)
	{
		try
		{
			var completed = await aIResourceData.DeleteAsync(id);
			return Results.Ok(completed);
		}
		catch (Exception ex)
		{
			return Results.Problem(ex.Message);
		}
	}
}
