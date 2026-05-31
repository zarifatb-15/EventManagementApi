using System.Net;
using WebApplicationApi.Helpers;

namespace WebApplicationApi;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }

    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 

        var errors = new List<string> { "An unexpected error occurred.", exception.Message };
        
        var responseModel = ResponseModelHelper.CreateErrorResponse<string>(errors);
        
        return context.Response.WriteAsJsonAsync(responseModel);
    }
}