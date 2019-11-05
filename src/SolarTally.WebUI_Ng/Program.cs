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
using Microsoft.Extensions.Logging;
using SolarTally.Application.System.Commands.SeedSampleData;
using SolarTally.Persistence;

namespace SolarTally.WebUI_Ng
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Trying to init database");

                try
                {
                    var solarTallyDbContext = services
                        .GetRequiredService<SolarTallyDbContext>();
                    solarTallyDbContext.Database.Migrate();
                    
                    // var mediator = services.GetRequiredService<IMediator>();
                    // mediator.Send(new SeedSampleDataCommand(),
                    //     CancellationToken.None);
                    
                    var seeder = new CustomSeeder(solarTallyDbContext);
                    seeder.SeedAll();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, @"An error occurred while initializing 
                        the database.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
