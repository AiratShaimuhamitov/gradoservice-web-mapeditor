using GradoService.Domain.Entities.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Persistence.Configuration
{
    public class TableFileConfiguration : IEntityTypeConfiguration<MetaTableFileInfo>
    {
        public void Configure(EntityTypeBuilder<MetaTableFileInfo> builder)
        {
            builder.ToTable("table_photo_info", "sys_scheme");

            builder.HasKey(k => k.Id);

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.TableId).HasColumnName("id_table");

            builder.Property(e => e.TableName).HasColumnName("photo_table");

            builder.Property(e => e.FileField).HasColumnName("photo_field");

            builder.Property(e => e.FieldFile).HasColumnName("photo_file");

            builder.Property(e => e.IdFieldTable).HasColumnName("id_field_tble");

            builder.Ignore(e => e.ViewNameFile);

            builder.Ignore(e => e.ViewNamePhoto);

            builder.HasOne(e => e.TableInfo)
                .WithMany(x => x.FileInfos)
                .HasForeignKey(x => x.TableId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("table_photo_info_id_table_fkey");
        }
    }
}
