using System.Reflection;
using Microsoft.EntityFrameworkCore;

using TopicManagementService.Infrastructure.Db.DbEntities;

namespace TopicManagementService.Infrastructure.Db;

public class TopicManagementDbContext : DbContext
{
    public DbSet<TopicDbEntity> Topics { get; set; }

    public TopicManagementDbContext(DbContextOptions<TopicManagementDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}