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
}
