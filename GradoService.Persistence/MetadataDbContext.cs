using GradoService.Domain.Entities.Metadata;
using Microsoft.EntityFrameworkCore;

namespace GradoService.Persistence
{
    public class MetadataDbContext : DbContext
    {
        public MetadataDbContext(DbContextOptions<MetadataDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<MetaTableInfo> TableInfos { get; set; }
        public DbSet<MetaTableFieldInfo> TableFieldInfos { get; set; }
        public DbSet<MetaTableFieldType> TableFieldTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MetadataDbContext).Assembly);
        }
    }
}