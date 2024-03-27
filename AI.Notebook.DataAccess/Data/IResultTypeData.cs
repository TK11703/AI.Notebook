using AI.Notebook.Common.Models;

namespace AI.Notebook.DataAccess.Data;
public interface IResultTypeData
{
	Task<bool> DeleteAsync(int id);
	Task<ResultTypeModel?> GetAsync(int id);
	Task<IEnumerable<ResultTypeModel>> GetAllAsync();
	Task<int> InsertAsync(ResultTypeModel item);
	int Update(ResultTypeModel item);
}