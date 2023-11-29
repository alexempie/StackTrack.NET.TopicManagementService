using TopicManagementService.Core.Entities;

namespace TopicManagementService.Core.Repositories.Interfaces;

public interface ITopicRepository
{
    Task<Topic> GetTopicByIdAsync(Guid topicId);

    Task<IEnumerable<Topic>> GetAllTopicsAsync();

    Task<IEnumerable<Topic>> GetBaseTopicsAsync();

    Task<IEnumerable<Topic>> GetChildSuptopicsAsync(Guid topicId);

    Task<IEnumerable<Topic>> GetDescendantSubtopicsAsync(Guid topicId);
}