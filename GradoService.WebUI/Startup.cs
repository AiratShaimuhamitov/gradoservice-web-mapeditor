using AutoMapper;
using System.Net.Http;
using GradoService.Application.ConfigurationModels;
using GradoService.Application.Interfaces;
using GradoService.Infrastructure.Services;
using GradoService.Persistence;
using GradoService.WebUI.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Reflection;
using GradoService.Application.Infrastructure;
using GradoService.Application.Infrastructure.AutoMapper;
using MediatR;
using GradoService.Application.Metadata.Queries.GetAllMetadata;
using GradoService.Persistence.Mapping.Profiles;
using GradoService.Persistence.CommandBuilder;

namespace GradoService.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add AutoMapper
            services.AddAutoMapper(new Assembly[]
            {
                typeof(AutoMapperProfile).GetTypeInfo().Assembly,
                typeof(DbMapperProfile).GetTypeInfo().Assembly
            });

            // Add MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddMediatR(typeof(GetAllMetadataQueryHandler).GetTypeInfo().Assembly);

            services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(ApiExceptionFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.Configure<ExternalAuthenticationConfig>(Configuration.GetSection("ExternalAuthenticationConfig"));


            services.AddSingleton<GradoServiceDbContextFactory>();

            // Add DbContext using GradoServiceDbContextFactory
            services.AddTransient(provider =>
            {
                var dbContextFactory = provider.GetService<GradoServiceDbContextFactory>();
                return dbContextFactory.CreateDbContext(new[] {""});
            });

            // Add Table Repository
            services.AddScoped<TableRepository>();

            // Add sql command building
            services.AddSingleton<SqlCommandBuilder, PostgresSqlCommandBuilder>();
            services.AddTransient<CrudCommandDirector>();
            

            services.AddSingleton<HttpClient>();
            services.AddTransient<IAuthenticationService, ExternalAuthenticationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
