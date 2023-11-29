using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TopicManagementService.Common.Helpers;
using TopicManagementService.Common.Services.Interfaces;
using TopicManagementService.Core.Repositories.Interfaces;
using TopicManagementService.Infrastructure.AppInfrastructure.Constants;
using TopicManagementService.Infrastructure.Db;
using TopicManagementService.Infrastructure.Mappers;
using TopicManagementService.Infrastructure.Repositories;
using TopicManagementService.Infrastructure.Services;
using CommonInfrastructureConstants = TopicManagementService.Common.Constants.InfrastructureContstants;

namespace TopicManagementService.Infrastructure.AppInfrastructure;

public class InfrastructureServiceRegistration : IServiceRegistration
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(TopicMappingProfile));

        services.AddScoped<IErrorMessageService, ErrorMessageService>();
        var connectionString = configuration.BuildSqlServerConnectionString
        (
                CommonInfrastructureConstants.DbConnectionStringParameterName,
                InfrastructureConstants.DbServerParameterName,
                InfrastructureConstants.DbNameParameterName,
                InfrastructureConstants.DbUserUsernameParameterName,
                InfrastructureConstants.DbUserPasswordParameterName
        );
        
        services.AddDbContext<TopicManagementDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<ITopicRepository, TopicRepository>();
    }
}