using GradoService.Domain.Entities.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Persistence.Configuration
{
    public class TableGeomTypeConfiguration : IEntityTypeConfiguration<MetaTableGeomType>
    {
        public void Configure(EntityTypeBuilder<MetaTableGeomType> builder)
        {
            
            builder.ToTable("table_type_geom", "sys_scheme");

            builder.HasKey(k => k.Id);

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.Name).HasColumnName("name");

            builder.Property(e => e.Type).HasColumnName("namedb");
        }
    }
}
