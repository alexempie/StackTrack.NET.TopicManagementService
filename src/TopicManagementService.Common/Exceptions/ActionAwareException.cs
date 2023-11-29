namespace TopicManagementService.Common.Exceptions;

public class ActionAwareException : Exception
{
    public string ActionName { get; }

    public ActionAwareException(string message, Exception innerException, string actionName)
        : base(message, innerException)
    {
        ActionName = actionName;
    }
}