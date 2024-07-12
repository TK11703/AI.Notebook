using AI.Notebook.Common.Models;

namespace AI.Notebook.DataAccess.Data;
public interface IRequestData
{
	Task<bool> DeleteAsync(int id);
	Task<RequestModel?> GetAsync(int id);
	Task<RequestTranslatorModel?> GetTranslatorAsync(int requestId);
	Task<RequestVisionModel?> GetVisionAsync(int requestId);
	Task<RequestSpeechModel?> GetSpeechAsync(int requestId);
	Task<RequestLanguageModel?> GetLanguageAsync(int requestId);
	Task<IEnumerable<RequestModel>> GetAllAsync();

	Task<PageResultModel<RequestModel>> GetPagedAsync(PageSubmissionModel pageRequest);
	Task<int> InsertAsync(RequestModel item);
	Task<int> InsertTranslatorAsync(RequestTranslatorModel item);
	Task<int> InsertSpeechAsync(RequestSpeechModel item);
	Task<int> InsertVisionAsync(RequestVisionModel item);
	Task<int> InsertLanguageAsync(RequestLanguageModel item);
	int Update(RequestModel item);
	int UpdateTranslator(RequestTranslatorModel item);
	int UpdateSpeech(RequestSpeechModel item);
	int UpdateVision(RequestVisionModel item);
	int UpdateLanguage(RequestLanguageModel item);
}