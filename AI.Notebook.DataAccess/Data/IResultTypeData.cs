using AI.Notebook.Common.Entities;

namespace AI.Notebook.DataAccess.Data;
public interface IResultTypeData
{
	Task<bool> DeleteAsync(int id);
	Task<ResultType?> GetAsync(int id);
	Task<ResultType?> GetByNameAsync(string name);
	Task<IEnumerable<ResultType>> GetAllAsync();
	Task<int> InsertAsync(ResultType item);
	int Update(ResultType item);
}