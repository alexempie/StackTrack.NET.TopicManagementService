using TopicManagementService.Common.Exceptions;

namespace TopicManagementService.Infrastructure.Exceptions;

public class DatabaseException : ActionAwareException
{
    public DatabaseException(string message, Exception innerException, string actionName)
        : base(message, innerException, actionName) { }
}