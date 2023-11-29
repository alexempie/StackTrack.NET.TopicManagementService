using System.Net;

using TopicManagementService.API.Services.Interfaces;

namespace TopicManagementService.API.Services;

public class HttpExceptionHandler : IHttpExceptionHandler
{
    public async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = exception switch
        {
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            StatusCode = (int)statusCode,
            Message = exception.Message
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}