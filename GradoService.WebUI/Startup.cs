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
            services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(ApiExceptionFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                }); ;

            services.Configure<ExternalAuthenticationConfig>(Configuration.GetSection("ExternalAuthenticationConfig"));


            services.AddSingleton<MetadataDbContextFactory>();

            // Add DbContext using MetadataDbContextFactory
            services.AddTransient(provider =>
            {
                var dbContextFactory = provider.GetService<MetadataDbContextFactory>();
                return dbContextFactory.CreateDbContext(new[] {""});
            });


            services.AddSingleton<HttpClient>();
            services.AddTransient<IAuthenticationService, ExternalAuthenticationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            ConfigureLogging(app, env, loggerFactory);
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public void ConfigureLogging(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.RollingFile("logs\\log-{Date}.log")
                .CreateLogger();

            if (env.IsDevelopment())
            {
                loggerFactory
                    .AddDebug()
                    .AddConsole()
                    .AddSerilog();
            }
            else
            {
                loggerFactory.AddSerilog();
            }
        }
    }
}
