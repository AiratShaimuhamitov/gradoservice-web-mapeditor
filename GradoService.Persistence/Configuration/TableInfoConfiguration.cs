using System;
using System.Collections.Generic;
using System.Text;
using GradoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GradoService.Persistence.Configuration
{
    public class TableInfoConfiguration : IEntityTypeConfiguration<TableInfo>
    {
        public void Configure(EntityTypeBuilder<TableInfo> builder)
        {
            builder.ToTable("table_info", "sys_scheme");

            builder.HasKey(k => k.Id);

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.SchemeMap).HasColumnName("scheme_name");

            builder.Property(e => e.Name).HasColumnName("name_db");

            builder.Property(e => e.PresentationName).HasColumnName("name_map");
        }
    }
}
