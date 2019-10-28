using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarTally.Domain.Entities;

namespace SolarTally.Persistence.Configurations
{
    public class ConsumptionConfiguration :
        IEntityTypeConfiguration<Consumption>
    {
        public void Configure(
            EntityTypeBuilder<Consumption> consumptionConfiguration)
        {   
            consumptionConfiguration.Ignore(b => b.DomainEvents);

            // I don't have a reference back to our Consumption, should be ok.
            // See https://docs.microsoft.com/en-us/ef/core/modeling/relationships#single-navigation-property
            // Here I'm declaring (I think) that when the Consumption is
            // deleted, the same should happen to all its ApplianceUsages.
            // ApplianceUsage does not have a nav back to Consumption, but
            // see https://docs.microsoft.com/en-us/ef/core/modeling/relationships#without-navigation-property
            // If confused about WithOne/WithMany, HasMany/HasOne
            // see https://stackoverflow.com/a/48692824
            consumptionConfiguration
                .HasMany<ApplianceUsage>(c => c.ApplianceUsages)
                .WithOne()
                .HasForeignKey(au => au.ConsumptionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            consumptionConfiguration.OwnsOne(c => c.ConsumptionTotal);
        }
    }
}