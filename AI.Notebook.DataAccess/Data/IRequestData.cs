using AI.Notebook.Common.Entities;

namespace AI.Notebook.DataAccess.Data;
public interface IRequestData
{
	Task<bool> DeleteLanguageAsync(int id);
	Task<bool> DeleteSpeechAsync(int id);
	Task<bool> DeleteTranslatorAsync(int id);
	Task<bool> DeleteVisionAsync(int id);
	Task<LanguageRequest?> GetLanguageAsync(int id);
	Task<PageResult<RequestBase>> GetPagedAsync(PageRequest pageRequest);
	Task<SpeechRequest?> GetSpeechAsync(int id);
	Task<TranslatorRequest?> GetTranslatorAsync(int id);
	Task<VisionRequest?> GetVisionAsync(int id);
	Task<int> InsertLanguageAsync(LanguageRequest item);
	Task<int> InsertSpeechAsync(SpeechRequest item);
	Task<int> InsertTranslatorAsync(TranslatorRequest item);
	Task<int> InsertVisionAsync(VisionRequest item);
	int UpdateLanguage(LanguageRequest item);
	int UpdateSpeech(SpeechRequest item);
	int UpdateTranslator(TranslatorRequest item);
	int UpdateVision(VisionRequest item);
}