using AI.Notebook.Common.Entities;

namespace AI.Notebook.Web.Clients;

public sealed class AIResourcesClient : IAIResourcesClient
{
	private readonly HttpClient _httpClient;
	private readonly ILogger<AIResourcesClient> _logger;

	public AIResourcesClient(HttpClient httpClient, ILogger<AIResourcesClient> logger)
	{
		_httpClient = httpClient;
		_logger = logger;
	}

	public async Task<IEnumerable<AIResource>?> GetAll()
	{
		using HttpResponseMessage response = await _httpClient.GetAsync("");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<IEnumerable<AIResource>>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return Enumerable.Empty<AIResource>();
	}

	public async Task<AIResource?> Get(int id)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"{id}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<AIResource>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return null;
	}

	public async Task<int> Create(AIResource item)
	{
		using HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"", item);
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<int>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return 0;
	}

	public async Task Update(AIResource item, int id)
	{
		using HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{id}", item);
		if (!response.IsSuccessStatusCode)
		{
			_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		}
	}

	public async Task Delete(int id)
	{
		using HttpResponseMessage response = await _httpClient.DeleteAsync($"{id}");
		if (!response.IsSuccessStatusCode)
		{
			_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		}
	}
}
