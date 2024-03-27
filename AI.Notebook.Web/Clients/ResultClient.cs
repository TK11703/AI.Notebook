using AI.Notebook.Common.Models;

namespace AI.Notebook.Web.Clients;

public sealed class ResultClient
{
	private readonly HttpClient _httpClient;
	private readonly ILogger<ResultClient> _logger;
	public ResultClient(HttpClient httpClient, ILogger<ResultClient> logger)
	{
		_httpClient = httpClient;
		_logger = logger;
	}

	public async Task<IEnumerable<ResultModel>?> GetAll()
	{
		using HttpResponseMessage response = await _httpClient.GetAsync("");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<IEnumerable<ResultModel>>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return Enumerable.Empty<ResultModel>();
	}

	public async Task<ResultModel?> Get(int id)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"{id}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<ResultModel>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return null;
	}

	public async Task<IEnumerable<ResultModel>?> GetByRequest(int id)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"RequestId/{id}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<IEnumerable<ResultModel>>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return Enumerable.Empty<ResultModel>();
	}

	public async Task<IEnumerable<ResultModel>?> GetByResource(int id)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"ResourceId/{id}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<IEnumerable<ResultModel>>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return Enumerable.Empty<ResultModel>();
	}

	public async Task<int> Create(ResultModel item)
	{
		using HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"", item);
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<int>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return 0;
	}

	public async Task Update(ResultModel item)
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
