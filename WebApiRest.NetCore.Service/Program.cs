using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;

namespace WebApiRest.NetCore.Service
{
    /// <summary>
    /// ...
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ...
        /// </summary>
        /// <param name="args">...</param>
        public static void Main(string[] args)
        {
            ConfigSerilog(@"applog.txt");

            try
            {
                Log.Information("Starting up the service.");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "There was a proble starting the service.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// ...
        /// </summary>
        /// <param name="args">...</param>
        /// <returns>...</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<MainWorker>();
                })
                .UseSerilog();

        /// <summary>
        /// ...
        /// </summary>
        /// <param name="pathLogFile">...</param>
        private static void ConfigSerilog(string pathLogFile)
        {
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                            .Enrich.FromLogContext()
                            .WriteTo.File(pathLogFile)
                            .CreateLogger();
        }
    }
}