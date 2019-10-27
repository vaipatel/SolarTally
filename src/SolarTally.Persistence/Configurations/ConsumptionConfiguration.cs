using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarTally.Domain.Entities;

namespace SolarTally.Persistence.Configurations
{
    public class ConsumptionConfiguration :
        IEntityTypeConfiguration<Consumption>
    {
        public void Configure(EntityTypeBuilder<Consumption>
            consumptionConfiguration)
        {   
            consumptionConfiguration.Ignore(b => b.DomainEvents);

            // I don't have a reference back to our Consumption, should be ok.
            // See https://docs.microsoft.com/en-us/ef/core/modeling/relationships#single-navigation-property
            consumptionConfiguration
                .HasMany<ApplianceUsage>(c => c.ApplianceUsages);

            consumptionConfiguration.OwnsOne(c => c.ConsumptionTotal);
        }
    }
}