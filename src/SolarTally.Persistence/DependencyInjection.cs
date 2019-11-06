using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SolarTally.Application.Common.Interfaces;

namespace SolarTally.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {
            var builder = new NpgsqlConnectionStringBuilder(
                configuration.GetConnectionString("SolarTallyDb")
            );
            if (isDevelopment)
            {
                builder.Username = configuration["PostgresUsername"];
                builder.Password = configuration["PostgresPassword"];
            }
            System.Console.WriteLine("\nPersistence username: {0}\n",
                builder.Username);

            services.AddDbContext<SolarTallyDbContext>(options =>
                options.UseNpgsql(builder.ConnectionString)
            );

            services.AddScoped<ISolarTallyDbContext>(provider =>
                provider.GetService<SolarTallyDbContext>()
            );

            return services;
        }
    }
}