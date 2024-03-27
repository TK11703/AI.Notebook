using AI.Notebook.DataAccess.DBAccess;
using AI.Notebook.Common.Models;
using System.Data;
using Dapper;

namespace AI.Notebook.DataAccess.Data;
public class ResultData : IResultData
{
	private readonly ISqlDataAccess _dataAccess;

	public ResultData(ISqlDataAccess dataAccess)
	{
		_dataAccess = dataAccess;
	}

	public async Task<IEnumerable<ResultModel>> GetByRequestAsync(int requestId)
	{
		var results = await _dataAccess.LoadDataAsync<ResultModel, dynamic>("dbo.spResults_GetByRequest", new { RequestId = requestId });
		if(results != null)
		{
			AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
			IEnumerable<AIResourceModel> aiResources = await aiResourceDataItems.GetAllAsync();
			ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
			IEnumerable<ResultTypeModel> resultTypes = await resultTypeDataItems.GetAllAsync();
			foreach (var result in results)
			{
				result.AIResource = aiResources.First(x => x.Id.Equals(result.ResourceId));
				result.ResultType = resultTypes.First(x => x.Id.Equals(result.ResultTypeId));
			}
			return results;
		}
		return Enumerable.Empty<ResultModel>();
	}

	public async Task<IEnumerable<ResultModel>> GetByResourceAsync(int resourceId)
	{
		var results = await _dataAccess.LoadDataAsync<ResultModel, dynamic>("dbo.spResults_GetByResource", new { ResourceId = resourceId });
		if (results != null)
		{
			AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
			IEnumerable<AIResourceModel> aiResources = await aiResourceDataItems.GetAllAsync();
			ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
			IEnumerable<ResultTypeModel> resultTypes = await resultTypeDataItems.GetAllAsync();
			foreach (var result in results)
			{
				result.AIResource = aiResources.First(x => x.Id.Equals(result.ResourceId));
				result.ResultType = resultTypes.First(x => x.Id.Equals(result.ResultTypeId));
			}
			return results;
		}
		return Enumerable.Empty<ResultModel>();
	}

	public async Task<ResultModel?> GetAsync(int id)
	{
		var results = await _dataAccess.LoadDataAsync<ResultModel, dynamic>("dbo.spResults_Get", new { Id = id });
		if(results != null)
		{
			var result = results.FirstOrDefault();
			if(result != null)
			{
				AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
				result.AIResource = await aiResourceDataItems.GetAsync(result.ResourceId);
				ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
				result.ResultType = await resultTypeDataItems.GetAsync(result.ResultTypeId);
			}
			return result;
		}
		return null;
	}

	public async Task<int> InsertAsync(ResultModel item)
	{
		var p = new DynamicParameters();
		p.Add(name: "@ResourceId", item.ResourceId);
		p.Add(name: "@RequestId", item.RequestId);
		p.Add(name: "@ResultTypeId", item.ResultTypeId);
		p.Add(name: "@ResultData", item.ResultData);
		p.Add(name: "@CompletedDt", item.CompletedDt);
		p.Add(name: "@Id", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResults_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public int Update(ResultModel item)
	{
		return _dataAccess.SaveData<dynamic>("dbo.spResults_Update", new { item.Id, item.ResourceId, item.RequestId, item.ResultTypeId, item.ResultData, item.CompletedDt });
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResults_Delete", p);
		var completed = p.Get<int?>("@Output");
		if (completed.HasValue && completed.Value.Equals(1))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
