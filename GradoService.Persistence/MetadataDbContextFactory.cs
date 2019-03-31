using System;
using System.Collections.Generic;
using System.Text;
using GradoService.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GradoService.Persistence
{
    public class MetadataDbContextFactory : DesignTimeDbContextFactoryBase<MetadataDbContext>
    {
        protected override MetadataDbContext CreateNewInstance(DbContextOptions<MetadataDbContext> options)
        {
            return new MetadataDbContext(options);
        }
    }
}
