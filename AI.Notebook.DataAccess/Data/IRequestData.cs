using AI.Notebook.Common.Entities;

namespace AI.Notebook.DataAccess.Data;
public interface IRequestData
{
	Task<bool> DeleteAsync(int id);
	Task<RequestBase?> GetAsync(int id);
	Task<TranslatorRequest?> GetTranslatorAsync(int requestId);
	Task<VisionRequest?> GetVisionAsync(int requestId);
	Task<SpeechRequest?> GetSpeechAsync(int requestId);
	Task<LanguageRequest?> GetLanguageAsync(int requestId);
	Task<IEnumerable<RequestBase>> GetAllAsync();

	Task<PageResult<RequestBase>> GetPagedAsync(PageRequest pageRequest);
	Task<int> InsertAsync(RequestBase item);
	Task<int> InsertTranslatorAsync(TranslatorRequest item);
	Task<int> InsertSpeechAsync(SpeechRequest item);
	Task<int> InsertVisionAsync(VisionRequest item);
	Task<int> InsertLanguageAsync(LanguageRequest item);
	int Update(RequestBase item);
	int UpdateTranslator(TranslatorRequest item);
	int UpdateSpeech(SpeechRequest item);
	int UpdateVision(VisionRequest item);
	int UpdateLanguage(LanguageRequest item);
}