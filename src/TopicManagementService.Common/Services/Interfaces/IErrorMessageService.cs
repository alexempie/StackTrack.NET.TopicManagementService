using TopicManagementService.Common.Enums;

namespace TopicManagementService.Common.Services.Interfaces;

public interface IErrorMessageService
{
    string GetErrorMessage(ErrorMessageCode errorCode);
}