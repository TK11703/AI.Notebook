using AI.Notebook.Common.AI.Text;
using AI.Notebook.Common.Models;

namespace AI.Notebook.DataAccess.Data;
public interface IResultData
{
	Task<bool> DeleteAsync(int id);
	Task<IEnumerable<ResultModel>> GetByRequestAsync(int requestId);
	Task<IEnumerable<ResultModel>> GetByResourceAsync(int resourceId);
	Task<ResultModel?> GetAsync(int id);
	Task<ResultTranslatorModel?> GetTranslatorAsync(int resultId);
	Task<ResultVisionModel?> GetVisionAsync(int resultId);
	Task<ResultSpeechModel?> GetSpeechAsync(int resultId);
	Task<ResultLanguageModel?> GetLanguageAsync(int resultId);
	Task<int> InsertAsync(ResultModel item);
	Task<int> InsertTranslatorAsync(ResultTranslatorModel item);
	Task<int> InsertSpeechAsync(ResultSpeechModel item);
	Task<int> InsertVisionAsync(ResultVisionModel item);
	Task<int> InsertLanguageAsync(ResultLanguageModel item);

	int Update(ResultModel item);
}