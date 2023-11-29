namespace TopicManagementService.Core.Entities;

public class Topic
{
    public Guid TopicId { get; set; }

    public Guid? ParentTopicId { get; set; }

    public string Title { get; set; }
}