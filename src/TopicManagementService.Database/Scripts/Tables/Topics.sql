CREATE TABLE [dbo].[Topics] (
    [TopicId] UNIQUEIDENTIFIER PRIMARY KEY,
    [ParentTopicId] UNIQUEIDENTIFIER NULL,
    [Title] NVARCHAR(255) NOT NULL,
    FOREIGN KEY ([ParentTopicId]) REFERENCES [dbo].[Topics]([TopicId]),
    CONSTRAINT [UQ_Topics_Title] UNIQUE ([Title])
);
GO