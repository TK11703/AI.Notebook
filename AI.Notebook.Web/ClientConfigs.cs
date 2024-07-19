using AI.Notebook.Web.Clients;

namespace AI.Notebook.Web;

public static class ClientConfigs
{
	/// <summary>
	/// Helps configure these TYPED clients (which are transient) to be used in potential singleton services and not be affected by DNS refresh issues and port exhaustion. Otherwise 
	/// NAMED clients could be used instead with the HttpClientFactory usage.
	/// </summary>
	/// <param name="services"></param>
	/// <param name="config"></param>
	public static void ConfigureClients(this IServiceCollection services, ConfigurationManager config)
	{
		services.AddHttpClient<RequestsClient>(client =>
		{
			client.BaseAddress = new Uri(config.GetValue<string>("Clients:RequestsBaseUri"));
		})
		.ConfigurePrimaryHttpMessageHandler(() =>
		{
			return new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(5)
			};
		})
		.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

		services.AddHttpClient<ResultsClient>(client =>
		{
			client.BaseAddress = new Uri(config.GetValue<string>("Clients:ResultsBaseUri"));
		})
		.ConfigurePrimaryHttpMessageHandler(() =>
		{
			return new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(5)
			};
		})
		.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

		services.AddHttpClient<AIResourcesClient>(client =>
		{
			client.BaseAddress = new Uri(config.GetValue<string>("Clients:AIResourcesBaseUri"));
		})
		.ConfigurePrimaryHttpMessageHandler(() =>
		{
			return new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(5)
			};
		})
		.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

		services.AddHttpClient<ResultTypesClient>(client =>
		{
			client.BaseAddress = new Uri(config.GetValue<string>("Clients:ResultTypesBaseUri"));
		})
		.ConfigurePrimaryHttpMessageHandler(() =>
		{
			return new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(5)
			};
		})
		.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

		services.AddHttpClient<SpeechClient>(client =>
		{
			client.BaseAddress = new Uri(config.GetValue<string>("Clients:SpeechBaseUri"));
		})
		.ConfigurePrimaryHttpMessageHandler(() =>
		{
			return new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(5)
			};
		})
		.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

		services.AddHttpClient<LanguageClient>(client =>
		{
			client.BaseAddress = new Uri(config.GetValue<string>("Clients:LanguageBaseUri"));
		})
		.ConfigurePrimaryHttpMessageHandler(() =>
		{
			return new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(5)
			};
		})
		.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

		services.AddHttpClient<VisionClient>(client =>
		{
			client.BaseAddress = new Uri(config.GetValue<string>("Clients:VisionBaseUri"));
		})
		.ConfigurePrimaryHttpMessageHandler(() =>
		{
			return new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(5)
			};
		})
		.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

		services.AddHttpClient<TranslatorClient>(client =>
		{
			client.BaseAddress = new Uri(config.GetValue<string>("Clients:TranslatorBaseUri"));
		})
		.ConfigurePrimaryHttpMessageHandler(() =>
		{
			return new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(5)
			};
		})
		.SetHandlerLifetime(Timeout.InfiniteTimeSpan);
	}
}
