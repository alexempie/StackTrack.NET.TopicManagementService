using Microsoft.Extensions.Configuration;
using SmartFormat;

namespace TopicManagementService.Common.Helpers;

public static class InfrastructureHelper
{
    public static string BuildSqlServerConnectionString(
        this IConfiguration configuration,
        string dbConnectionStringParameterName,
        string dbServerParameterName,
        string dbNameParameterName,
        string dbUserUsernameParameterName,
        string dbUserPasswordParameterName
    ){
        var connectionString = 
            configuration.GetConnectionString(dbConnectionStringParameterName);
        
        var connectionStringParametersData = new 
        { 
            DbServer = configuration[dbServerParameterName],
            DbName = configuration[dbNameParameterName], 
            DbUserUsername = configuration[dbUserUsernameParameterName], 
            DbUserPassword = configuration[dbUserPasswordParameterName]
        };

        return Smart.Format(connectionString, connectionStringParametersData); 
    }
}