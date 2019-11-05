using Microsoft.EntityFrameworkCore;
using SolarTally.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SolarTally.Application.Common.Interfaces
{
    public interface ISolarTallyDbContext
    {
        DbSet<Site> Sites { get; set; }
        DbSet<Consumption> Consumptions { get; set; }
        DbSet<ApplianceUsage> ApplianceUsages { get; set; }
        DbSet<Appliance> Appliances { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        int SaveChanges(bool acceptAllChangesOnSuccess);
    }
}