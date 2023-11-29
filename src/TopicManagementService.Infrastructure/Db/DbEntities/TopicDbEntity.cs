namespace TopicManagementService.Infrastructure.Db.DbEntities;

public class TopicDbEntity
{
    public Guid TopicId { get; set; }

    public Guid? ParentTopicId { get; set; }

    public string Title { get; set; }
}