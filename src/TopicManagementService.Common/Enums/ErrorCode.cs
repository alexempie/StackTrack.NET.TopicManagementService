namespace TopicManagementService.Common.Enums;

public enum ErrorMessageCode
{
    UnexpectedError,
    DatabaseConnectionFailure,
    DatabaseTimeout,
    ValidationFailure,
    UnauthorizedAccess,
    ExternalServiceFailure,
    TopicNotFound,
    DuplicateTopic,
    InvalidParentTopic,
    InternalServerError,
    ServiceUnavailable
}