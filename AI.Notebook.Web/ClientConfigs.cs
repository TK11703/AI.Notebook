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
		services.AddHttpClient<RequestClient>(client =>
		{
			client.BaseAddress = new Uri(config.GetValue<string>("Clients:RequestModelBaseUri"));
		})
		.ConfigurePrimaryHttpMessageHandler(() =>
		{
			return new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(5)
			};
		})
		.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

		services.AddHttpClient<ResultClient>(client =>
		{
			client.BaseAddress = new Uri(config.GetValue<string>("Clients:ResultModelBaseUri"));
		})
		.ConfigurePrimaryHttpMessageHandler(() =>
		{
			return new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(5)
			};
		})
		.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

		services.AddHttpClient<AIResourceClient>(client =>
		{
			client.BaseAddress = new Uri(config.GetValue<string>("Clients:AIResourceModelBaseUri"));
		})
		.ConfigurePrimaryHttpMessageHandler(() =>
		{
			return new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(5)
			};
		})
		.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

		services.AddHttpClient<ResultTypeClient>(client =>
		{
			client.BaseAddress = new Uri(config.GetValue<string>("Clients:ResultTypeModelBaseUri"));
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
