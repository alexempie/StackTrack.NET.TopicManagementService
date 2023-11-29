using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TopicManagementService.Common.Services.Interfaces;

public interface IServiceRegistration
{
    void RegisterServices(IServiceCollection services, IConfiguration configuration);
}