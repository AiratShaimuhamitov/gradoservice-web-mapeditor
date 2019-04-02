using System;
using System.Collections.Generic;
using System.Text;
using GradoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GradoService.Persistence
{
    public class MetadataDbContext : DbContext
    {
        public MetadataDbContext(DbContextOptions<MetadataDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<TableInfo> TableInfos { get; set; }
        public DbSet<TableFieldInfo> TableFieldInfos { get; set; }
        public DbSet<TableFieldType> TableFieldTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MetadataDbContext).Assembly);
        }
    }
}