DECLARE @parent1 AS UNIQUEIDENTIFIER = NEWID();
DECLARE @parent2 AS UNIQUEIDENTIFIER = NEWID();
DECLARE @child1 AS UNIQUEIDENTIFIER = NEWID();

INSERT INTO [dbo].[Topics] ([TopicId], [Title])
VALUES (@parent1, 'Parent Topic 1'),
       (@parent2, 'Parent Topic 2');

INSERT INTO [dbo].[Topics] ([TopicId], [ParentTopicId], [Title])
VALUES (@child1, @parent2, 'Child 1 of Topic 2'),
       (NEWID(), @parent2, 'Child 2 of Topic 2'),
       (NEWID(), @parent2, 'Child 3 of Topic 2');

INSERT INTO [dbo].[Topics] ([TopicId], [ParentTopicId], [Title])
VALUES (NEWID(), @child1, 'Child 1 of Child 1 of Topic 2'),
       (NEWID(), @child1, 'Child 2 of Child 1 of Topic 2');
GO