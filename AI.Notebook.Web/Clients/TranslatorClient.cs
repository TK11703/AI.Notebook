using AI.Notebook.Common.AI.Text;
using AI.Notebook.Common.Entities;

namespace AI.Notebook.Web.Clients;

public sealed class TranslatorClient
{
	private readonly HttpClient _httpClient;
	private readonly ILogger<TranslatorClient> _logger;

	public TranslatorClient(HttpClient httpClient, ILogger<TranslatorClient> logger)
	{
		_httpClient = httpClient;
		_logger = logger;
	}

	public async Task<SupportedLanguagesResult?> GetTranslatorLanguages()
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"Languages");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<SupportedLanguagesResult>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");

		return null;
	}

	public async Task<TextTranslationResult?> Translate(TranslatorRequest request)
	{
		using HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"Translate", request);
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<TextTranslationResult>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");

		return null;
	}

	public async Task<byte[]?> GenerateSsml(SsmlRequest request)
	{
		using HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"GenerateSsmlAudio", request);
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<byte[]?>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");

		return null;
	}

	public async Task<TextTranslationResult?> Transliterate(TranslatorRequest request)
	{
		using HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"Transliterate", request);
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<TextTranslationResult>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");

		return null;
	}

	public async Task<int> Create(TranslatorRequest item)
	{

		using HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"Request", item);
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<int>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return 0;
	}

	public async Task Update(TranslatorRequest item)
	{
		using HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"Request/{item.Id}", item);
		if (!response.IsSuccessStatusCode)
		{
			_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		}
	}

	public async Task<bool> DeleteRequest(int id)
	{
		using HttpResponseMessage response = await _httpClient.DeleteAsync($"Request/{id}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<bool>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");

		return false;
	}

	public async Task<bool> DeleteResult(int id)
	{
		using HttpResponseMessage response = await _httpClient.DeleteAsync($"Result/{id}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<bool>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");

		return false;
	}

	public async Task<TranslatorRequest?> GetTranslatorRequest(int id)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"Request/{id}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<TranslatorRequest>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return null;
	}

	public async Task<IEnumerable<ResultBase>?> GetTranslatorResultsByRequest(int requestId)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"Results/Request/{requestId}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<IEnumerable<ResultBase>>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return Enumerable.Empty<ResultBase>();
	}

	public async Task<TranslatorResult?> GetTranslationResult(int id)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"Result/{id}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<TranslatorResult>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return null;
	}
}
