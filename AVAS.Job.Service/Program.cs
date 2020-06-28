using AVAS.Job.Core.Clients;
using AVAS.Job.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using System;
using System.IO;

namespace AVAS.Job.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {

                    // Build configuration
                    var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                        .AddJsonFile("appsettings.json", false)
                        .Build();

                    // Add access to generic IConfigurationRoot
                    services.AddSingleton<IConfiguration>(configuration);
                    services.AddTransient<IVideoQueueService, VideoQueueService>();
                    services.AddTransient<IYoutubeDLClient, YoutubeDLClient>();
                    services.AddTransient<IDownloadService, DownloadService>();

                    services.AddHostedService<Worker>()
                      .Configure<EventLogSettings>(config =>
                      {
                          config.LogName = "AVAS";
                          config.SourceName = "AVAS";
                      });
                })
            .UseWindowsService();
    }
}
