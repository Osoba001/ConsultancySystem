using ConsultancySystem.WebApi.Models;
using System.Net;

namespace LCS.WebApi.CustomMiddlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(new ErrorDetail
                {
                    StatusCode = context.Response.StatusCode,
                    Message = $"Internal Serval Error: {ex.Message}."
                }.ToString());
                _logger.LogError($"Something went wrong. Unhandle exception: \n{ex}");
            }
        }
    }

}
