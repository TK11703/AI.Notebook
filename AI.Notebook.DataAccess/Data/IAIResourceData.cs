using AI.Notebook.Common.Entities;

namespace AI.Notebook.DataAccess.Data;
public interface IAIResourceData
{
	Task<bool> DeleteAsync(int id);
	Task<AIResource?> GetAsync(int id);
	Task<IEnumerable<AIResource>> GetAllAsync();
	Task<int> InsertAsync(AIResource item);
	int Update(AIResource item);
}