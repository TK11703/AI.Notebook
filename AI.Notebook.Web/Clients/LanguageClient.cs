using AI.Notebook.Common.AI.Text;
using AI.Notebook.Common.Entities;

namespace AI.Notebook.Web.Clients;

public sealed class LanguageClient
{
	private readonly HttpClient _httpClient;
	private readonly ILogger<LanguageClient> _logger;

	public LanguageClient(HttpClient httpClient, ILogger<LanguageClient> logger)
	{
		_httpClient = httpClient;
		_logger = logger;
	}

	public async Task<int> Create(LanguageRequest item)
	{
		using HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"Request", item);
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<int>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return 0;
	}

	public async Task Update(LanguageRequest item)
	{
		using HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"Request/{item.Id}", item);
		if (!response.IsSuccessStatusCode)
		{
			_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		}
	}

	public async Task<LanguageRequest?> GetLanguageRequest(int id)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"Request/{id}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<LanguageRequest>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return null;
	}

	public async Task<LanguageResult?> GetLanguageResult(int id)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"Result/{id}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<LanguageResult>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return null;
	}
}
