using AI.Notebook.DataAccess.DBAccess;
using AI.Notebook.Common.Models;
using Dapper;
using System.Data;

namespace AI.Notebook.DataAccess.Data;
public class RequestData : IRequestData
{
	private readonly ISqlDataAccess _dataAccess;

	public RequestData(ISqlDataAccess dataAccess)
	{
		_dataAccess = dataAccess;
	}

	public async Task<IEnumerable<RequestModel>> GetAllAsync()
	{
		var requests = await _dataAccess.LoadDataAsync<RequestModel, dynamic>("dbo.spRequests_GetAll", new { });
		if (requests != null)
		{
			AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
			IEnumerable<AIResourceModel> aiResources = await aiResourceDataItems.GetAllAsync();
			foreach (var request in requests)
			{
				request.AIResource = aiResources.First(x => x.Id == request.ResourceId);
			}
			return requests;
		}

		return Enumerable.Empty<RequestModel>();
	}

	public async Task<PageResultModel<RequestModel>> GetPagedAsync(PageSubmissionModel pageRequest)
	{
		PageResultModel<RequestModel> results = new PageResultModel<RequestModel>(pageRequest.PageSize, pageRequest.Start);
		var p = new DynamicParameters();
		p.Add(name: "@SortBy", pageRequest.SortBy);
		p.Add(name: "@SortOrder", pageRequest.SortDirection);
		p.Add(name: "@PageSize", pageRequest.PageSize);		
		p.Add(name: "@Start", pageRequest.Start);
		p.Add(name: "@PageSize", pageRequest.PageSize);
		if(!string.IsNullOrEmpty(pageRequest.Filter))
		{
			p.Add(name: "@Search", pageRequest.Filter);
		}
		if (pageRequest.BeginDate.HasValue)
		{
			p.Add(name: "@Begin", pageRequest.BeginDate.Value.Date);
		}
		if (pageRequest.EndDate.HasValue)
		{
			p.Add(name: "@End", pageRequest.EndDate.Value.Date);
		}
		p.Add(name: "@Total", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		var requests = await _dataAccess.LoadDataAsync<RequestModel, dynamic>("dbo.spRequests_GetPaged", p);
		if(requests != null)
		{
			results.ItemCount = p.Get<int>("@Total");
			AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
			IEnumerable<AIResourceModel> aiResources = await aiResourceDataItems.GetAllAsync();
			foreach(var request in requests)
			{
				request.AIResource = aiResources.First(x=>x.Id == request.ResourceId);
			}
			results.Collection = requests;
			return results;
		}
		
		return new PageResultModel<RequestModel>();
	}

	public async Task<RequestModel?> GetAsync(int id)
	{
		var results = await _dataAccess.LoadDataAsync<RequestModel, dynamic>("dbo.spRequests_Get", new { Id = id });
		if (results != null)
		{
			var request = results.FirstOrDefault();
			if(request != null) 
			{
				AIResourceData data = new AIResourceData(_dataAccess);
				request.AIResource = await data.GetAsync(request.ResourceId);
			}
			return request;
		}
		return null;
	}

	public async Task<int> InsertAsync(RequestModel item)
	{
		var p = new DynamicParameters();
		p.Add(name: "@ResourceId", item.ResourceId);
		p.Add(name: "@Name", item.Name);
		p.Add(name: "@Input", item.Input);
		p.Add(name: "@Id", value:0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spRequests_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public int Update(RequestModel item)
	{
		return _dataAccess.SaveData<dynamic>("dbo.spRequests_Update", new { item.Id, item.ResourceId, item.Name, item.Input });
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spRequests_Delete", p);
		var completed = p.Get<int?>("@Output");
		if(completed.HasValue && completed.Value.Equals(1))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
