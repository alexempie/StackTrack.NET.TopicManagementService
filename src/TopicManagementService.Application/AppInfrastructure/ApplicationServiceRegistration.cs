using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TopicManagementService.Application.Mappers;
using TopicManagementService.Common.Services.Interfaces;

namespace TopicManagementService.Application.AppInfrastructure;

public class ApplicationServiceRegistration : IServiceRegistration
{
    [Obsolete]
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(TopicMappingProfile));
        
        services.AddFluentValidation(fv => 
            fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}