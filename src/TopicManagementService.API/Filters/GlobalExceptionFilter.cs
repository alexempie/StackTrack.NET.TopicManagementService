using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using TopicManagementService.Common.Enums;
using TopicManagementService.Common.Exceptions;
using TopicManagementService.Common.Services.Interfaces;
using TopicManagementService.Infrastructure.Exceptions;

namespace TopicManagementService.API.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly IErrorMessageService _errorMessageService;

    public GlobalExceptionFilter(
        ILogger<GlobalExceptionFilter> logger,
        IWebHostEnvironment environment,
        IErrorMessageService errorMessageService)
    {
        _logger = logger;
        _environment = environment;
        _errorMessageService = errorMessageService;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var actionAwareException = exception as ActionAwareException;
        var actionName = actionAwareException?.ActionName ?? context.ActionDescriptor.DisplayName;
        var detailedMessage = $"{actionName}: {exception.Message}";

        _logger.LogError(exception, detailedMessage);

        var (statusCode, errorMessageCode) 
            = GetStatusAndErrorMessageCodeByException(exception);

        var problemDetails = new ProblemDetails
        {
            Title = _errorMessageService.GetErrorMessage(errorMessageCode),
            Status = statusCode,
            Instance = context.HttpContext.Request.Path,
            Detail = _environment.IsDevelopment() ? detailedMessage : null
        };

        context.Result = new ObjectResult(new { error = problemDetails })
        {
            StatusCode = problemDetails.Status,
        };
    }

    private (int, ErrorMessageCode) GetStatusAndErrorMessageCodeByException(Exception exception) => exception switch
    {
        DatabaseException => (StatusCodes.Status503ServiceUnavailable, ErrorMessageCode.ServiceUnavailable),
        _ => (StatusCodes.Status500InternalServerError, ErrorMessageCode.InternalServerError)
    };
}