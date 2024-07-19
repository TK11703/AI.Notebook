using AI.Notebook.Common.Entities;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace AI.Notebook.Web.Clients;

public sealed class RequestsClient
{
	private readonly HttpClient _httpClient;
	private readonly ILogger<RequestsClient> _logger;

	public RequestsClient(HttpClient httpClient, ILogger<RequestsClient> logger)
	{
		_httpClient = httpClient;
		_logger = logger;
	}

	public async Task<IEnumerable<RequestBase>> GetAll()
	{
		using HttpResponseMessage response = await _httpClient.GetAsync("");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<IEnumerable<RequestBase>>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return Enumerable.Empty<RequestBase>();
	}

	public async Task<PageResult<RequestBase>> GetAll(PageRequest pagedRequest)
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
			return await response.Content.ReadFromJsonAsync<PageResult<RequestBase>>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return new PageResult<RequestBase>();
	}

	public async Task<RequestBase?> Get(int id)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync($"{id}");
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<RequestBase>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return null;
	}

	public async Task<int> Create(RequestBase item)
	{
		using HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"", item);
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadFromJsonAsync<int>();
		}
		_logger.LogError($"Http Status: {response.StatusCode}{Environment.NewLine}Http Message: {await response.Content.ReadAsStringAsync()}");
		return 0;
	}

	public async Task Update(RequestBase item)
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
