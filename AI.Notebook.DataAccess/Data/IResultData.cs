using AI.Notebook.Common.Entities;

namespace AI.Notebook.DataAccess.Data;
public interface IResultData
{
	Task<bool> DeleteLanguageResultAsync(int id);
	Task<bool> DeleteSpeechResultAsync(int id);
	Task<bool> DeleteTranslatorResultAsync(int id);
	Task<bool> DeleteVisionResultAsync(int id);
	Task<LanguageResult?> GetLanguageAsync(int id);
	Task<PageResult<ResultBase>> GetPagedAsync(PageRequest pageRequest);
	Task<SpeechResult?> GetSpeechAsync(int id);
	Task<TranslatorResult?> GetTranslatorAsync(int id);
	Task<VisionResult?> GetVisionAsync(int id);
	Task<int> InsertLanguageAsync(LanguageResult item);
	Task<int> InsertSpeechAsync(SpeechResult item);
	Task<int> InsertTranslatorAsync(TranslatorResult item);
	Task<int> InsertVisionAsync(VisionResult item);
	int UpdateLanguage(int id, int resultTypeId, string resultText, DateTime? completedDate);
	int UpdateSpeech(int id, int resultTypeId, string resultText, byte[] resultAudio, DateTime? completedDate);
	int UpdateTranslator(int id, int resultTypeId, string resultText, byte[] resultAudio, DateTime? completedDate);
	int UpdateVision(int id, int resultTypeId, string resultText, DateTime? completedDate);

	Task<IEnumerable<ResultBase>?> GetTranslatorResultsByRequestAsync(int requestId);
	Task<IEnumerable<ResultBase>?> GetVisionResultsByRequestAsync(int requestId);
	Task<IEnumerable<ResultBase>?> GetSpeechResultsByRequestAsync(int requestId);
	Task<IEnumerable<ResultBase>?> GetLanguageResultsByRequestAsync(int requestId);
}