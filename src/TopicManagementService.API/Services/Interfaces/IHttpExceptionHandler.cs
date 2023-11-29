namespace TopicManagementService.API.Services.Interfaces;

public interface IHttpExceptionHandler
{
    Task HandleExceptionAsync(HttpContext context, Exception exception);
}