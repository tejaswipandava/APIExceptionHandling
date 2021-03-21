using System.Net;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Exceptionhandling.ExceptionHandling._Errors;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Exceptionhandling.ExceptionHandling._Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _env = env;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                //Writes the error to our output window
                _logger.LogError(error, error.Message);

                //Setting the status of the response
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                //used for displaying stack in dev environment and simple message in Production
                var response = (_env.IsDevelopment())
                ? new ApiExceptions(context.Response.StatusCode, error.Message, error.StackTrace?.ToString())
                : new ApiExceptions(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}