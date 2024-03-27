using AI.Notebook.Common.Models;

namespace AI.Notebook.DataAccess.Data;
public interface IResultData
{
	Task<bool> DeleteAsync(int id);
	Task<IEnumerable<ResultModel>> GetByRequestAsync(int requestId);
	Task<IEnumerable<ResultModel>> GetByResourceAsync(int resourceId);
	Task<ResultModel?> GetAsync(int id);
	Task<int> InsertAsync(ResultModel item);
	int Update(ResultModel item);
}