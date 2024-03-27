using AI.Notebook.Common.Models;

namespace AI.Notebook.Web.Clients;
public interface IAIResourceClient
{
	Task<int> Create(AIResourceModel item);
	Task Delete(int id);
	Task<AIResourceModel?> Get(int id);
	Task<IEnumerable<AIResourceModel>?> GetAll();
	Task Update(AIResourceModel item, int id);
}