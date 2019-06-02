namespace Infrastructure.Bootstrapper
{
	using System;

	using Infrastructure.Bootstrapper.Middlewares;

	using Microsoft.AspNetCore.Builder;

	using Infrastructure.Bootstrapper.Extensions;

	using Microsoft.AspNetCore.Routing;

	public static class WebApiBootstrapper
    {
		public static void Use(IApplicationBuilder builder, string route = "")
		{
			builder
				.UseMiddleware<ExceptionMiddlewareToLogError>()
				.UseCorsToAllowAll()
				.UseRouting(route);

		}

		public const string VersionedRoutePattern = "api/v{api-version:apiVersion}/[controller]/[action]";
    }
}
