using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using TopicManagementService.Infrastructure.Db;
using TopicManagementService.Common.Services.Interfaces;
using TopicManagementService.Infrastructure.Exceptions;
using TopicManagementService.Common.Enums;
using TopicManagementService.Core.Repositories.Interfaces;
using TopicManagementService.Core.Entities;
using TopicManagementService.Infrastructure.Db.Constants;

namespace TopicManagementService.Infrastructure.Repositories;

public class TopicRepository : ITopicRepository
{
    private readonly IMapper _mapper;
    private readonly IErrorMessageService _errorMessageService;
    private readonly TopicManagementDbContext _dbContext;

    public TopicRepository(
        IMapper mapper,
        IErrorMessageService errorMessageService,
        TopicManagementDbContext dbContext)
    {
        _mapper = mapper;
        _errorMessageService = errorMessageService;
        _dbContext = dbContext;
    }

    public async Task<Topic> GetTopicByIdAsync(Guid topicId)
    {
        const string actionName = nameof(GetTopicByIdAsync);
        try
        {
            var topicDbEntity 
                = await _dbContext.Topics.FirstOrDefaultAsync(x => x.ParentTopicId == topicId);

            return _mapper.Map<Topic>(topicDbEntity);
        }
        catch (SqlException ex)
        {
            var message = $"{_errorMessageService.GetErrorMessage(ErrorMessageCode.DatabaseConnectionFailure)}";
            throw new DatabaseException(message, ex, actionName);
        }
    }

    public async Task<IEnumerable<Topic>> GetAllTopicsAsync()
    {
        const string actionName = nameof(GetAllTopicsAsync);
        try
        {
            var topicsDbEntities = await _dbContext.Topics.ToListAsync();
            
            return _mapper.Map<IEnumerable<Topic>>(topicsDbEntities);
        }
        catch (SqlException ex)
        {
            var message = $"{_errorMessageService.GetErrorMessage(ErrorMessageCode.DatabaseConnectionFailure)}";
            throw new DatabaseException(message, ex, actionName);
        }
    }

    public async Task<IEnumerable<Topic>> GetBaseTopicsAsync()
    {
        const string actionName = nameof(GetBaseTopicsAsync);
        try
        {
            var topicsDbEntities 
                = await _dbContext.Topics.Where(x => x.ParentTopicId == null).ToListAsync();
            
            return _mapper.Map<IEnumerable<Topic>>(topicsDbEntities);
        }
        catch (SqlException ex)
        {
            var message = $"{_errorMessageService.GetErrorMessage(ErrorMessageCode.DatabaseConnectionFailure)}";
            throw new DatabaseException(message, ex, actionName);
        }
    }

    public async Task<IEnumerable<Topic>> GetChildSuptopicsAsync(Guid topicId)
    {
        const string actionName = nameof(GetChildSuptopicsAsync);
        try
        {
            var topicsDbEntities 
                = await _dbContext.Topics.Where(x => x.ParentTopicId == topicId).ToListAsync();
            
            return _mapper.Map<IEnumerable<Topic>>(topicsDbEntities);
        }
        catch (SqlException ex)
        {
            var message = $"{_errorMessageService.GetErrorMessage(ErrorMessageCode.DatabaseConnectionFailure)}";
            throw new DatabaseException(message, ex, actionName);
        }
    }

    public async Task<IEnumerable<Topic>> GetDescendantSubtopicsAsync(Guid topicId)
    {
        const string actionName = nameof(GetDescendantSubtopicsAsync);
        try
        {
            var parameter = new SqlParameter(DatabaseConstants.TopicIdParameterName, topicId);
            var topicsDbEntities = await _dbContext.Topics
                                                .FromSqlRaw($"EXECUTE {DatabaseConstants.GetDescendantSubtopicsProcedureName} @TopicId", parameter)
                                                .ToListAsync();

            return _mapper.Map<IEnumerable<Topic>>(topicsDbEntities);
        }
        catch (SqlException ex)
        {
            var message = $"{_errorMessageService.GetErrorMessage(ErrorMessageCode.DatabaseConnectionFailure)}";
            throw new DatabaseException(message, ex, actionName);
        }
        catch (InvalidOperationException ex)
        {
            var message = $"{_errorMessageService.GetErrorMessage(ErrorMessageCode.ValidationFailure)}";
            throw new RepositoryException(message, ex, actionName);
        }
    }
}