using AutoMapper;
using System.Net.Http;
using GradoService.Infrastructure.Models;
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
using Swashbuckle.AspNetCore.Swagger;

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
            //Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "GradoService MapEditor API", Version = "v1" });
            });

            // Add AutoMapper
            services.AddAutoMapper(new Assembly[]
            {
                typeof(AutoMapperProfile).GetTypeInfo().Assembly,
                typeof(DbMapperProfile).GetTypeInfo().Assembly
            });

            // Add MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateTableExistsPipelineBehavior<,>));
            services.AddMediatR(typeof(GetAllMetadataQueryHandler).GetTypeInfo().Assembly);

            services.ConfigureExceptionsHandling();

            services.ConfigurePersistence();

            services.ConfigureAuthentication(Configuration);
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

            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GradoService MapEditor API");
            });
        }
    }

    public static class Configurations
    {
        public static void ConfigurePersistence(this IServiceCollection services)
        {
            services.AddSingleton<GradoServiceDbContextFactory>();

            // Add DbContext using GradoServiceDbContextFactory
            services.AddTransient(provider =>
            {
                var dbContextFactory = provider.GetService<GradoServiceDbContextFactory>();
                return dbContextFactory.CreateDbContext(new[] { "" });
            });

            // Add sql commands building
            services.AddSingleton<SqlCommandBuilder, PostgresSqlCommandBuilder>();
            services.AddTransient<CrudCommandDirector>();

            // Add Repositories
            services.AddScoped<TableRepository>();
            services.AddScoped<FileRepository>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ExternalAuthenticationConfig>(configuration.GetSection("ExternalAuthenticationConfig"));

            services.AddSingleton<HttpClient>();
            services.AddTransient<IAuthenticationService, ExternalAuthenticationService>();
        }

        public static void ConfigureExceptionsHandling(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ApiExceptionFilter));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }
    }
}
