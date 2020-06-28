using AVAS.Job.Core.Clients;
using AVAS.Job.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AVAS.Job.App
{
    public class Program
    {
        public static IConfigurationRoot configuration;

        static int Main(string[] args)
        {
            try
            {
                // Start!
                MainAsync(args).Wait();
                return 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Polling App Failed: " + ex.Message);
                return 1;
            }
        }

        static async Task MainAsync(string[] args)
        {
            // Create service collection
            Console.WriteLine("Creating service collection");
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Create service provider
            Console.WriteLine("Building service provider");
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            // Print connection string to demonstrate configuration object is populated
            Console.WriteLine(configuration.GetConnectionString("DataConnection"));

            try
            {
                Console.WriteLine("Starting service");
                await serviceProvider.GetService<App>().Run();
                Console.WriteLine("Ending service");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error running service - " + ex.Message);
                throw ex;
            }

        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {

            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddTransient<IVideoQueueService, VideoQueueService>();
            serviceCollection.AddTransient<IYoutubeDLClient, YoutubeDLClient>();
            serviceCollection.AddTransient<IDownloadService, DownloadService>();

            // Add app
            serviceCollection.AddTransient<App>();
        }
    }
}
