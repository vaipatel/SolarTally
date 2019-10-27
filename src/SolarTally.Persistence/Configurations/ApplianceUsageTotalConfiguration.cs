using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarTally.Domain.ValueObjects;

namespace SolarTally.Persistence.Configurations
{
    /// <summary>
    /// Provides db configuration for the ApplianceUsageTotal value object.
    /// </summary>
    /// <remarks>
    /// It seems that while we <see href="https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities#by-design-restrictions">
    /// cannot have a DbSet for a value object by design</see>, we <see href="https://github.com/dotnet-architecture/eShopOnWeb/blob/master/src/Infrastructure/Data/Config/AddressConfiguration.cs">
    /// can have a configuration</see> <see href="https://github.com/dotnet-architecture/eShopOnWeb/blob/master/src/Infrastructure/Data/CatalogContext.cs#L40">
    /// for the value object</see>. This makes sense to me because the
    /// configuration can offer info on the data types and constraints.
    /// </remarks>
    public class ApplianceUsageTotalConfiguration :
        IEntityTypeConfiguration<ApplianceUsageTotal>
    {
        public void Configure(EntityTypeBuilder<ApplianceUsageTotal>
            autConfiguration)
        {
            autConfiguration.Property(aut => aut.TotalPowerConsumption)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            autConfiguration.Property(aut => aut.TotalEnergyConsumption)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}