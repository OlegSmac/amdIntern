using System.Net;
using Vehicles.API.Models;

namespace Vehicles.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var error = new Error()
            {
                StatusCode = context.Response.StatusCode.ToString(),
                Message = "Sorry, an error on the server occured"
            };
            
            await context.Response.WriteAsync(error.ToString());
        }
    }
}