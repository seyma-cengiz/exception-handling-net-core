using ExceptionHandlingMethods.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace ExceptionHandlingMethods.Extensions;

public static class ExceptionMiddlewareExtension
{
    //Built-in Exception Handler
    public static void ConfigureBuiltinExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                var contextRequest = context.Request;

                context.Response.ContentType = "application/json";
                if (contextFeature != null)
                {
                    var errorResponse = new ErrorResponse
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Message = contextFeature.Error.Message,
                        Path = contextRequest.Path
                    }.ToString();

                    await context.Response.WriteAsync(errorResponse);
                }
            });
        });
    }

    //Custom Exception Handler Middleware
    public static void ConfigureCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}
