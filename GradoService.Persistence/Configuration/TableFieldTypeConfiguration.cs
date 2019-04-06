using GradoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GradoService.Persistence.Configuration
{
    public class TableFieldTypeConfiguration : IEntityTypeConfiguration<TableFieldType>
    {
        public void Configure(EntityTypeBuilder<TableFieldType> builder)
        {
            builder.ToTable("table_type", "sys_scheme");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id).HasColumnName("id");

            builder.Property(p => p.Name).HasColumnName("name");

            builder.Property(p => p.Type).HasColumnName("name_db");
        }
    }
}
