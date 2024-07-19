using AI.Notebook.Common.Entities;

namespace AI.Notebook.Web.Clients;
public interface IAIResourcesClient
{
	Task<int> Create(AIResource item);
	Task Delete(int id);
	Task<AIResource?> Get(int id);
	Task<IEnumerable<AIResource>?> GetAll();
	Task Update(AIResource item, int id);
}