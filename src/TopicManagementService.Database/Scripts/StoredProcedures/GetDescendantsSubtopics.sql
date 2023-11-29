CREATE PROCEDURE [dbo].[GetDescendantSubtopics]
    @TopicId UNIQUEIDENTIFIER
AS
BEGIN
    WITH [Descendants] AS (
        SELECT [TopicId], [ParentTopicId], [Title]
        FROM [dbo].[Topics]
        WHERE [TopicId] = @TopicId
        UNION ALL
        SELECT t.[TopicId], t.[ParentTopicId], t.[Title]
        FROM [dbo].[Topics] t
        INNER JOIN [Descendants] d ON t.[ParentTopicId] = d.[TopicId]
    )
    SELECT * FROM [Descendants];
END;
GO