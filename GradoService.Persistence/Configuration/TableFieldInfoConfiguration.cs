using System;
using System.Collections.Generic;
using System.Text;
using GradoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GradoService.Persistence.Configuration
{
    public class TableFieldInfoConfiguration : IEntityTypeConfiguration<TableFieldInfo>
    {
        public void Configure(EntityTypeBuilder<TableFieldInfo> builder)
        {
            builder.ToTable("table_field_info", "sys_scheme");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.TableId).HasColumnName("id_table");

            builder.Property(e => e.Name).HasColumnName("name_db");

            builder.Property(e => e.PresentationName).HasColumnName("name_map");

            builder.Property(e => e.FieldTypeId).HasColumnName("type_field");

            builder.HasOne(e => e.Table)
                .WithMany(p => p.FieldInfos)
                .HasForeignKey(e => e.TableId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("table_field_info_id_table_fkey");

            builder.HasOne(e => e.FieldType)
                .WithMany(p => p.FieldInfos)
                .HasForeignKey(k => k.FieldTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("table_field_info_type_field_fkey");
        }
    }
}
