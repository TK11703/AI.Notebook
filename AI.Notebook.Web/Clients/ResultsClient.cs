using AI.Notebook.Common.Entities;
using System.Net.Mime;
using System.Text.Json;
using System.Text;

namespace AI.Notebook.Web.Clients;

public sealed class ResultsClient
{
	private readonly HttpClient _httpClient;
	private readonly ILogger<ResultsClient> _logger;
	public ResultsClient(HttpClient httpClient, ILogger<ResultsClient> logger)
	{
		_httpClient = httpClient;
		_logger = logger;
	}

	public async Task<IEnumerable<ResultBase>?> GetAll()
	{
		using HttpResponseMessage response = await _httpClient.GetAsync("");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<IEnumerable<ResultBase>>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return Enumerable.Empty<ResultBase>();
	}

	public async Task<PageResult<ResultBase>> GetAll(PageRequest pagedRequest)
	{
		var request = new HttpRequestMessage()
		{
			Method = HttpMethod.Get,
			RequestUri = new Uri(Path.Combine(_httpClient.BaseAddress.AbsoluteUri, "PagedRequest")),
			Content = new StringContent(JsonSerializer.Serialize(pagedRequest), Encoding.UTF8, MediaTypeNames.Application.Json)
		};
		using HttpResponseMessage response = await _httpClient.SendAsync(request);
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<PageResult<ResultBase>>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return new PageResult<ResultBase>();
	}

	public async Task<ResultBase?> Get(int id)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"{id}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<ResultBase>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return null;
	}

	public async Task<IEnumerable<ResultBase>?> GetByRequest(int id)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"Request/{id}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<IEnumerable<ResultBase>>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return Enumerable.Empty<ResultBase>();
	}

	public async Task<IEnumerable<ResultBase>?> GetByResource(int id)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"AIResource/{id}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<IEnumerable<ResultBase>>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return Enumerable.Empty<ResultBase>();
	}

	public async Task<int> Create(ResultBase item)
	{
		using HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"", item);
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<int>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return 0;
	}

	public async Task Update(ResultBase item)
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
