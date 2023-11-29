namespace TopicManagementService.Application.Entities.Dtos;

public class TopicDto
{
    public Guid TopicId { get; set; }

    public Guid? ParentTopicId { get; set; }

    public string Title { get; set; }
}