using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TopicManagementService.Infrastructure.Db.Constants;
using TopicManagementService.Infrastructure.Db.DbEntities;

namespace TopicManagementService.Infrastructure.Db.EntityConfigurations;

public class TopicEntityConfiguration : IEntityTypeConfiguration<TopicDbEntity>
{
    public void Configure(EntityTypeBuilder<TopicDbEntity> builder)
    {
        builder.ToTable(DatabaseConstants.TopicsTableName);
        builder.HasKey(t => t.TopicId);
        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasOne<TopicDbEntity>()
            .WithMany()
            .HasForeignKey(t => t.ParentTopicId);
    }
}