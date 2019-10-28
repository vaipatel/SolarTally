using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolarTally.Application.Common.Interfaces;

namespace SolarTally.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SolarTallyDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("SolarTallyDb"))
            );

            services.AddScoped<ISolarTallyDbContext>(provider =>
                provider.GetService<SolarTallyDbContext>()
            );

            return services;
        }
    }
}