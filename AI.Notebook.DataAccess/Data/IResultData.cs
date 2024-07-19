using AI.Notebook.Common.AI.Text;
using AI.Notebook.Common.Entities;

namespace AI.Notebook.DataAccess.Data;
public interface IResultData
{
	Task<bool> DeleteAsync(int id);
	Task<bool> DeleteTranslatorResultAsync(int id, int resultId);
	Task<bool> DeleteSpeechResultAsync(int id, int resultId);
	Task<bool> DeleteVisionResultAsync(int id, int resultId);
	Task<bool> DeleteLanguageResultAsync(int id, int resultId);
	
	Task<IEnumerable<ResultBase>> GetByRequestAsync(int requestId);
	Task<IEnumerable<ResultBase>> GetByResourceAsync(int resourceId);
	Task<ResultBase?> GetAsync(int id);

	Task<PageResult<ResultBase>> GetPagedAsync(PageRequest pageRequest);
	Task<TranslatorResult?> GetTranslatorAsync(int resultId);
	Task<VisionResult?> GetVisionAsync(int resultId);
	Task<SpeechResult?> GetSpeechAsync(int resultId);
	Task<LanguageResult?> GetLanguageAsync(int resultId);
	Task<int> InsertAsync(ResultBase item);
	Task<int> InsertTranslatorAsync(TranslatorResult item);
	Task<int> InsertSpeechAsync(SpeechResult item);
	Task<int> InsertVisionAsync(VisionResult item);
	Task<int> InsertLanguageAsync(LanguageResult item);

	int Update(ResultBase item);
}