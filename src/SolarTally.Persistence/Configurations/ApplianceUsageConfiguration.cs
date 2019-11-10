using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarTally.Domain.Entities;

namespace SolarTally.Persistence.Configurations
{
    public class ApplianceUsageConfiguration :
        IEntityTypeConfiguration<ApplianceUsage>
    {
        public void Configure(EntityTypeBuilder<ApplianceUsage> auConfiguration)
        {
            auConfiguration.Ignore(b => b.DomainEvents);
            
            auConfiguration.Property(au => au.Quantity)
                .HasColumnType("smallint")
                .HasDefaultValue<int>(1)
                .IsRequired();
            
            auConfiguration.Property(au => au.PowerConsumption)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue<decimal>(20.5)
                .IsRequired();
            
            auConfiguration.Property(au => au.NumHours)
                .HasColumnType("smallint")
                .HasDefaultValue<int>(8)
                .IsRequired();
            
            auConfiguration.Property(au => au.PercentHrsOnSolar)
                .HasColumnType("decimal(2,1)")
                .HasDefaultValue<decimal>(1.0)
                .IsRequired();

            auConfiguration.Property(au => au.NumHoursOnSolar)
                .HasColumnType("smallint")
                .HasDefaultValue<int>(8)
                .IsRequired();

            auConfiguration.Property(au => au.Enabled)
                .HasColumnType("boolean")
                .HasDefaultValue<bool>(true)
                .IsRequired();

            auConfiguration.OwnsOne(s => s.ApplianceUsageTotal);
        }
    }
}