﻿using GradoService.Domain.Entities.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GradoService.Persistence.Configuration
{
    public class TableInfoConfiguration : IEntityTypeConfiguration<MetaTableInfo>
    {
        public void Configure(EntityTypeBuilder<MetaTableInfo> builder)
        {
            builder.ToTable("table_info", "sys_scheme");

            builder.HasKey(k => k.Id);

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.Schema).HasColumnName("scheme_name");

            builder.Property(e => e.Name).HasColumnName("name_db");

            builder.Property(e => e.PresentationName).HasColumnName("name_map");

            builder.Property(e => e.Geom).HasColumnName("geom_field");

            builder.Property(e => e.StyleField).HasColumnName("style_field");

            builder.Property(e => e.GeomType).HasColumnName("geom_type");

            builder.Property(e => e.Type).HasColumnName("type");

            builder.Property(e => e.DefaultStyle).HasColumnName("default_style");

            builder.Property(e => e.ContainsDocument).HasColumnName("photo");

            builder.Property(e => e.ViewQuery).HasColumnName("sql_view_string");
        }
    }
}
