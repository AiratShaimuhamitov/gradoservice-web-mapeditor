using System;
using System.Collections.Generic;
using System.Text;
using GradoService.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GradoService.Persistence
{
    public class GradoServiceDbContextFactory : DesignTimeDbContextFactoryBase<GradoServiceDbContext>
    {
        protected override GradoServiceDbContext CreateNewInstance(DbContextOptions<GradoServiceDbContext> options)
        {
            return new GradoServiceDbContext(options);
        }
    }
}
