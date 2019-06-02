namespace Infrastructure.Bootstrapper.Middlewares
{
	using System;
	using System.Threading.Tasks;

	using Infrastructure.Logger.Contracts;

	using Microsoft.AspNetCore.Http;

	public class ExceptionMiddlewareToLogError
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddlewareToLogError(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context,ILog log)
        {
            try
            {
                await this._next(context);
            }
            catch (Exception ex)
            {
                log.Fatal(ex,"Failed to process the request");
                throw;
            }
           
        }
    }
}
