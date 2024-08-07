﻿using AI.Notebook.DataAccess.DBAccess;
using Dapper;
using System.Data;
using AI.Notebook.Common.Entities;

namespace AI.Notebook.DataAccess.Data;
public class ResultTypeData : IResultTypeData
{
	private readonly ISqlDataAccess _dataAccess;

	public ResultTypeData(ISqlDataAccess dataAccess)
	{
		_dataAccess = dataAccess;
	}

	public async Task<IEnumerable<ResultType>> GetAllAsync()
	{
		return await _dataAccess.LoadDataAsync<ResultType, dynamic>("dbo.spResultTypes_GetAll", new { });
	}

	public async Task<ResultType?> GetAsync(int id)
	{
		var results = await _dataAccess.LoadDataAsync<ResultType, dynamic>("dbo.spResultTypes_Get", new { Id = id });

		return results.FirstOrDefault();
	}

	public async Task<ResultType?> GetByNameAsync(string name)
	{
		var results = await _dataAccess.LoadDataAsync<ResultType, dynamic>("dbo.spResultTypes_GetByName", new { Name = name });

		return results.FirstOrDefault();
	}

	public async Task<int> InsertAsync(ResultType item)
	{
		var p = new DynamicParameters();		
		p.Add(name: "@Name", item.Name);
		p.Add(name: "@Description", item.Description);
		p.Add(name: "@Active", item.Active);
		p.Add(name: "@Id", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResultTypes_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public int Update(ResultType item)
	{
		return _dataAccess.SaveData<dynamic>("dbo.spResultTypes_Update", new { item.Id, item.Name, item.Description, item.Active });
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResultTypes_Delete", p);
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
