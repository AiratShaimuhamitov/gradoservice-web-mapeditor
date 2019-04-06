using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace GradoService.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (System.Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .ConfigureSerilogLogger()
                .UseSerilog();
    }

    public static class WebHostBuilderExtenstions
    {
        public static IWebHostBuilder ConfigureSerilogLogger(this IWebHostBuilder builder)
        {
            builder.ConfigureLogging((hostingContext, logger) =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.RollingFile("logs\\log-{Date}.log")
                    .CreateLogger();

                if (hostingContext.HostingEnvironment.IsDevelopment())
                {
                    logger
                        .AddDebug()
                        .AddConsole()
                        .AddSerilog();
                }
                else
                {
                    logger.AddSerilog();
                }
            });

            return builder;
        }
    }
}
