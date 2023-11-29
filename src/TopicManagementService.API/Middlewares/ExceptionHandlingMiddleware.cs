using TopicManagementService.API.Services.Interfaces;

namespace TopicManagementService.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpExceptionHandler  _httpExceptionHandler;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        IHttpExceptionHandler httpExceptionHandler)
    {
        _next = next;
        _httpExceptionHandler = httpExceptionHandler;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await _httpExceptionHandler.HandleExceptionAsync(httpContext, ex);
        }
    }
}