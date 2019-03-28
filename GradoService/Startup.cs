using System.Net.Http;
using GradoService.Application.ConfigurationModels;
using GradoService.Application.Interfaces;
using GradoService.Infrastructure.Services;
using GradoService.WebUI.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<ExternalAuthenticationConfig>(Configuration.GetSection("ExternalAuthenticationConfig"));

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
