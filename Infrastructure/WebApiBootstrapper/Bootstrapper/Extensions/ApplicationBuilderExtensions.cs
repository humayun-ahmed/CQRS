namespace Infrastructure.Bootstrapper.Extensions
{
	using System;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;

	public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRouting(this IApplicationBuilder applicationBuilder, string routing = "", Action<IRouteBuilder> routeBuilderAction = null)
        {
            if (routeBuilderAction == null)
            {
                routeBuilderAction = builder =>
                {
                    if (string.IsNullOrEmpty(routing))
                        routing = "api/{controller}/{action}/{id?}";
                    builder.MapRoute("default", routing);
                };
            }

            return UseRouting(applicationBuilder, routeBuilderAction);
        }
        public static IApplicationBuilder UseRouting(this IApplicationBuilder applicationBuilder, Action<IRouteBuilder> routeBuilderAction) => applicationBuilder.UseMvc(routeBuilderAction);
        public static IApplicationBuilder UseCorsToAllowAll(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            return applicationBuilder;
        }
    }
}
