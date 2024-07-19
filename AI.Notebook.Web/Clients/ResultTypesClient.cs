using AI.Notebook.Common.Entities;

namespace AI.Notebook.Web.Clients;

public sealed class ResultTypesClient
{
	private readonly HttpClient _httpClient;
	private readonly ILogger<ResultTypesClient> _logger;

	public ResultTypesClient(HttpClient httpClient, ILogger<ResultTypesClient> logger)
	{
		_httpClient = httpClient;
		_logger = logger;
	}

	public async Task<IEnumerable<ResultType>?> GetAll()
	{
		using HttpResponseMessage response = await _httpClient.GetAsync("");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<IEnumerable<ResultType>>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");

		return Enumerable.Empty<ResultType>();
	}

	public async Task<ResultType?> Get(int id)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"{id}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<ResultType>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return null;
	}

	public async Task<int> Create(ResultType item)
	{
		using HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"", item);
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<int>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return 0;
	}

	public async Task Update(ResultType item)
	{
		using HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{item.Id}", item);
		if (!response.IsSuccessStatusCode)
		{
			_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		}
	}

	public async Task Delete(int id)
	{
		using HttpResponseMessage response = await _httpClient.DeleteAsync($"?id={id}");
		if (!response.IsSuccessStatusCode)
		{
			_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		}
	}
}
