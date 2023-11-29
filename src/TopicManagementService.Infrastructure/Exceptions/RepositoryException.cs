using TopicManagementService.Common.Exceptions;

namespace TopicManagementService.Infrastructure.Exceptions;

public class RepositoryException : ActionAwareException
{
    public RepositoryException(string message, Exception innerException, string actionName)
        : base(message, innerException, actionName) { }
}