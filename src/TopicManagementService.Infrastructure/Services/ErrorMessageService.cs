using TopicManagementService.Common.Services.Interfaces;
using TopicManagementService.Common.Enums;

namespace TopicManagementService.Infrastructure.Services;

public class ErrorMessageService : IErrorMessageService
{
    private static readonly Dictionary<ErrorMessageCode, string> ErrorMessages = new Dictionary<ErrorMessageCode, string>
    {
        { ErrorMessageCode.UnexpectedError, "An unexpected error occurred." },
        { ErrorMessageCode.DatabaseConnectionFailure, "Failed to connect to the database." },
        { ErrorMessageCode.DatabaseTimeout, "Database operation timed out." },
        { ErrorMessageCode.ValidationFailure, "Validation failed." },
        { ErrorMessageCode.UnauthorizedAccess, "Unauthorized access." },
        { ErrorMessageCode.ExternalServiceFailure, "Failed to connect to the external service." },
        { ErrorMessageCode.TopicNotFound, "Topic not found." },
        { ErrorMessageCode.DuplicateTopic, "Topic already exists." },
        { ErrorMessageCode.InvalidParentTopic, "Invalid parent topic." },
        { ErrorMessageCode.ServiceUnavailable, "Service unavailable. Please try again later." },
        { ErrorMessageCode.InternalServerError, "An error occurred while processing the request." }
    };

    public string GetErrorMessage(ErrorMessageCode errorCode)
        => ErrorMessages.TryGetValue(errorCode, out var message) ? message : "Unknown error.";
}