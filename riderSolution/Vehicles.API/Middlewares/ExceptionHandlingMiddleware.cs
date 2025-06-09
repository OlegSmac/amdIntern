using System.Net;
using Vehicles.API.Contracts;

namespace Vehicles.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    //private readonly ILogger _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var error = new Error()
            {
                StatusCode = context.Response.StatusCode.ToString(),
                Message = e.Message
            };
            
            await context.Response.WriteAsync(error.ToString());
        }
    }
}