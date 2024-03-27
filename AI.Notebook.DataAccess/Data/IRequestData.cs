using AI.Notebook.Common.Models;

namespace AI.Notebook.DataAccess.Data;
public interface IRequestData
{
	Task<bool> DeleteAsync(int id);
	Task<RequestModel?> GetAsync(int id);
	Task<IEnumerable<RequestModel>> GetAllAsync();

	Task<PageResultModel<RequestModel>> GetPagedAsync(PageSubmissionModel pageRequest);
	Task<int> InsertAsync(RequestModel item);
	int Update(RequestModel item);
}