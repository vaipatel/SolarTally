using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SolarTally.Domain.Entities;
using SolarTally.Application.Common.Interfaces;

namespace SolarTally.Persistence
{
    public class SolarTallyDbContext : DbContext, ISolarTallyDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        // private readonly IDateTime _dateTime;

        public SolarTallyDbContext(
            DbContextOptions<SolarTallyDbContext> options)
            : base(options)
        {
        }

        public SolarTallyDbContext(
            DbContextOptions<SolarTallyDbContext> options, 
            ICurrentUserService currentUserService) //,
            // IDateTime dateTime)
            : base(options)
        {
            _currentUserService = currentUserService;
            // _dateTime = dateTime;
        }

        public DbSet<Site> Sites { get; set; }

        public DbSet<Consumption> Consumptions { get; set; }

        public DbSet<ApplianceUsage> ApplianceUsages { get; set; }

        public DbSet<Appliance> Appliances { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            // {
            //     switch (entry.State)
            //     {
            //         case EntityState.Added:
            //             entry.Entity.CreatedBy = _currentUserService.UserId;
            //             entry.Entity.Created = _dateTime.Now;
            //             break;
            //         case EntityState.Modified:
            //             entry.Entity.LastModifiedBy = _currentUserService.UserId;
            //             entry.Entity.LastModified = _dateTime.Now;
            //             break;
            //     }
            // }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(SolarTallyDbContext).Assembly);
        }
    }
}