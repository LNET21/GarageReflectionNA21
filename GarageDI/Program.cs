using ConsoleUI;
using GarageDI.Entities;
using GarageDI.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using VehicleCollection;

namespace GarageDI
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            serviceProvider.GetService<GarageManager>().Run();
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            IConfiguration configuration = GetConfig();
            ISettings garageSettings = new Settings();
 
            configuration.Bind("Garage:Settings", garageSettings);

            serviceCollection.AddSingleton(configuration);
            serviceCollection.AddSingleton(garageSettings);
            serviceCollection.AddTransient<GarageManager>();
            serviceCollection.AddTransient<IGarage<IVehicle>, InMemoryGarage<IVehicle>>();
            serviceCollection.AddTransient<IVehicle, Vehicle>();
            serviceCollection.AddTransient<IGarageHandler, GarageHandler>();
            serviceCollection.AddTransient<IUI, ConsoleUI.ConsoleUI>();
        }

        private static IConfigurationRoot GetConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }
    }
}
