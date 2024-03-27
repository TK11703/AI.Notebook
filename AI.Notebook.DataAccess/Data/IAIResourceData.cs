using AI.Notebook.Common.Models;

namespace AI.Notebook.DataAccess.Data;
public interface IAIResourceData
{
	Task<bool> DeleteAsync(int id);
	Task<AIResourceModel?> GetAsync(int id);
	Task<IEnumerable<AIResourceModel>> GetAllAsync();
	Task<int> InsertAsync(AIResourceModel item);
	int Update(AIResourceModel item);
}