using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
/// Vai: Uncomment the below for storing db creds in Azure Key Vault.
// using Microsoft.Extensions.Configuration.AzureKeyVault;
// using Microsoft.Azure.KeyVault;
// using Microsoft.Azure.Services.AppAuthentication;
using SolarTally.Persistence;

namespace SolarTally.WebUI_Ng
{
    public class Program
    {
        private static bool IsEnvDev = false;

        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Trying to init database");

                if (IsEnvDev)
                {
                    try
                    {
                        var solarTallyDbContext = services
                            .GetRequiredService<SolarTallyDbContext>();
                        solarTallyDbContext.Database.Migrate();
                        if (!solarTallyDbContext.Sites.Any())
                        {
                            logger.LogInformation("Will seed database!");
                            var seeder = new CustomSeeder(solarTallyDbContext);
                            seeder.SeedAll();
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, @"An error occurred while 
                            initializing the database.");
                    }
                } else
                {
                    logger.LogInformation("Skipping db seed for non-dev env.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    /// Vai: I will store whether the env is Dev or not here.
                    IsEnvDev = context.HostingEnvironment.IsDevelopment();

                    /// Vai: If we want to keep the db username/pwd out of the
                    /// connection string in production, we can keep it in 
                    /// Azure Key Vault like below.
                    // if (context.HostingEnvironment.IsProduction())
                    // {
                    //     var builtConfig = config.Build();

                    //     var azureServiceTokenProvider = new AzureServiceTokenProvider();
                    //     var keyVaultClient = new KeyVaultClient(
                    //         new KeyVaultClient.AuthenticationCallback(
                    //             azureServiceTokenProvider.KeyVaultTokenCallback));

                    //     config.AddAzureKeyVault(
                    //         $"https://{builtConfig["KeyVaultName"]}.vault.azure.net/",
                    //         keyVaultClient,
                    //         new DefaultKeyVaultSecretManager());
                    // }
                })
                .UseStartup<Startup>();
    }
}
