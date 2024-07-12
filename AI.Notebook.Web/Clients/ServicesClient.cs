using AI.Notebook.Common.AI.Text;
using AI.Notebook.Common.Models;

namespace AI.Notebook.Web.Clients;

public sealed class ServicesClient
{
	private readonly HttpClient _httpClient;
	private readonly ILogger<ServicesClient> _logger;

	public ServicesClient(HttpClient httpClient, ILogger<ServicesClient> logger)
	{
		_httpClient = httpClient;
		_logger = logger;
	}

	public async Task<SupportedLanguagesResult?> GetTranslatorLanguages()
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"Translator/GetLangauges");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<SupportedLanguagesResult>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");

		return null;
	}

	public async Task<SupportedLanguagesResult?> Translate(RequestTranslatorModel request)
	{
		using HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"Translator/Translate", request);
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<SupportedLanguagesResult>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");

		return null;
	}

	public async Task<SupportedLanguagesResult?> Transliterate(RequestTranslatorModel request)
	{
		using HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"Translator/Transliterate", request);
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<SupportedLanguagesResult>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");

		return null;
	}
}
