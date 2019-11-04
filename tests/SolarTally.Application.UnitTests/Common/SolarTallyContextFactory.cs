using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SolarTally.Domain.Entities;
using SolarTally.Persistence;

namespace SolarTally.Application.UnitTests.Common
{
    /// <summary>
    /// Creates a SolarTallyDbContext.
    /// </summary>
    /// <remarks>
    /// Note how the name is SolarTallyContextFactory, not
    /// SolarTallyDbContextFactory (we're omitting the Db).
    /// </remarks>
    public class SolarTallyContextFactory
    {
        public static SolarTallyDbContext Create()
        {
            var options = new DbContextOptionsBuilder<SolarTallyDbContext>()
                // .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseSqlite("DataSource=:memory:")
                .Options;
            
            var context = new SolarTallyDbContext(options);

            // Need to do for SQLite.
            context.Database.OpenConnection();

            context.Database.EnsureCreated();

            var appliances = new[] {
                new Appliance("LED", "An LED Bulb", 20),
                new Appliance("Washer", "A Frontload Washer", 800),
                new Appliance("Car", "A Tesla", 2000)
            };
            context.Appliances.AddRange(appliances);
            // TODO: Is this okay to do?
            // context.SaveChanges();

            var site = new Site("PetroCanada Station", 7);
            site.MainAddress = new Domain.ValueObjects.Address("0 Yonge St.",
            "Toronto", "Ontario", "Canada", "M1N2O3");

            foreach(var appliance in appliances)
            {
                site.Consumption.AddApplianceUsage(appliance);
                var au = site.Consumption.ApplianceUsages.Last();
                au.SetQuantity(2);
            }
            context.Sites.Add(site);
            context.Consumptions.Add(site.Consumption);
            context.ApplianceUsages.AddRange(site.Consumption.ApplianceUsages);

            context.SaveChanges();

            return context;
        }

        public static void Destroy(SolarTallyDbContext context)
        {
            // For SQLite, I'm guessing I need to do this on Destroy().
            context.Database.CloseConnection();

            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
