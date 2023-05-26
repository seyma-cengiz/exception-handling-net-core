using ExceptionHandlingMethods.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ExceptionHandlingMethods.Exceptions.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        context.HttpContext.Response.ContentType = "application/json";

        var error = new ErrorResponse
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = context.Exception.Message,
            Path = context.HttpContext.Request.Path
        };

        context.Result = new JsonResult(error);
    }
}
