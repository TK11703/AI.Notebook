
namespace AI.Notebook.API;

/// <summary>
/// Great solution for a minimal API (MicroServices) where you want the program.cs file kept clean. 
/// Invoke in the program file like: app.ConfigureApis();
/// </summary>
public static class Api
{
	public static void ConfigureApis(this WebApplication application)
	{
		application.MapGet("/AIResources", GetAllAsync);
		application.MapGet("/AIResources/{id}", GetAsync);
		application.MapPost("/AIResources", InsertAsync);
		application.MapPut("/AIResources/{id}", Update);
		application.MapDelete("/AIResources", DeleteAsync); 
	}

	private static async Task<IResult> GetAllAsync(IAIResourceData aIResourceData)
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

	private static async Task<IResult> GetAsync(int id, IAIResourceData aIResourceData)
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

	private static async Task<IResult> InsertAsync(AIResourceModel aIResourceModel, IAIResourceData aIResourceData)
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

	private static IResult Update(int id, AIResourceModel aIResourceModel, IAIResourceData aIResourceData)
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

	private static async Task<IResult> DeleteAsync(int id, IAIResourceData aIResourceData)
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
