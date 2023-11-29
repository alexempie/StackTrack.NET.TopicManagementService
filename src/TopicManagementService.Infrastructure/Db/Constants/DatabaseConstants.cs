namespace TopicManagementService.Infrastructure.Db.Constants;

public static class DatabaseConstants
{   
    public const string TopicsTableName = "Topics";

    public const string GetDescendantSubtopicsProcedureName = "dbo.GetDescendantSubtopics";

    public const string TopicIdParameterName = "@TopicId";
}