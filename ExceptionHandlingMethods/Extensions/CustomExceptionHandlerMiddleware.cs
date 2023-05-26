using ExceptionHandlingMethods.Models;
using System.Net;

namespace ExceptionHandlingMethods.Extensions;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        var error = new ErrorResponse
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = ex.Message,
            Path = context.Request.Path
        }.ToString();
        await context.Response.WriteAsync(error);
    }
}
