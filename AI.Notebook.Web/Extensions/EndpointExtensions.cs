using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace AI.Notebook.Web.Extensions;

public static class EndpointExtensions
{
	public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
	{
		ServiceDescriptor[] endpointServiceDescriptors = assembly.DefinedTypes
			.Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IFileHandler)))
			.Select(type => ServiceDescriptor.Transient(typeof(IFileHandler), type))
			.ToArray();
		services.TryAddEnumerable(endpointServiceDescriptors);
		return services;
	}

	public static IApplicationBuilder MapFileHandlerEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
	{
		IEnumerable<IFileHandler> endpoints = app.Services.GetRequiredService<IEnumerable<IFileHandler>>();

		IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

		foreach (IFileHandler endpoint in endpoints)
		{
			endpoint.MapEndpoint(builder);
		}
		return app;
	}
}