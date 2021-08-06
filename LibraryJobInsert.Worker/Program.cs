using LibraryJobInsert.Application.Extensions;
using LibraryJobInsert.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace LibraryJobInsert.Worker
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
                    IConfigurationBuilder configBuilderForMain = new ConfigurationBuilder();

                    ConfigureConfiguration(configBuilderForMain);

                    IConfiguration configForMain = configBuilderForMain.Build();

                    services.AddApplicationServicesExtensions(configForMain);

                    services.AddInfrastructureServicesExtensios(configForMain);

                    services.AddHostedService<Worker>();

                });

        public static void ConfigureConfiguration(IConfigurationBuilder config)
        {
            config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        }
    }
}
