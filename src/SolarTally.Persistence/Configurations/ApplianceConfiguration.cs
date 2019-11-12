using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarTally.Domain.Entities;

namespace SolarTally.Persistence.Configurations
{
    public class ApplianceConfiguration : IEntityTypeConfiguration<Appliance>
    {
        public void Configure(
            EntityTypeBuilder<Appliance> applianceConfiguration)
        {
            applianceConfiguration.Ignore(b => b.DomainEvents);

            applianceConfiguration.Property(a => a.Name)
                .HasMaxLength(50)
                .HasDefaultValue("Appliance Name")
                .IsRequired();
            
            applianceConfiguration.Property(a => a.Description)
                .HasMaxLength(50)
                .HasDefaultValue("Appliance Description")
                .IsRequired(false);

            applianceConfiguration.Property(a => a.DefaultPowerConsumption)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue<decimal>(30.00)
                .IsRequired();

            applianceConfiguration
                .Property(a => a.DefaultStartupPowerConsumption)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue<decimal>(60.00)
                .IsRequired();
        }
    }
}