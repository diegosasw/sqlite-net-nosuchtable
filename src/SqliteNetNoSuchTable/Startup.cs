using System;
using Microsoft.Extensions.DependencyInjection;

namespace SqliteNetNoSuchTable
{
    public static class Startup
    {
        public static IServiceProvider Init(string databasePath)
        {
            var serviceProviderOptions =
                new ServiceProviderOptions
                {
                    ValidateOnBuild = true,
                    ValidateScopes = true
                };

            var serviceProvider =
                new ServiceCollection()
                    .AddSingleton(sp => new Database(databasePath))
                    .AddSingleton<DatabaseManager>()
                    .AddSingleton<MyRepository>()
                    .AddTransient<MyFunctionality>()
                    .BuildServiceProvider(serviceProviderOptions);

            var databaseManager = serviceProvider.GetService<DatabaseManager>();
            databaseManager.TryCreate().Wait();

            return serviceProvider;
        }
    }
}
