using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarTally.Domain.ValueObjects;

namespace SolarTally.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> addressConfiguration)
        {
            addressConfiguration.Property(a => a.ZipCode)
                .HasMaxLength(18)
                .IsRequired();

            addressConfiguration.Property(a => a.Street)
                .HasMaxLength(180)
                .IsRequired();

            addressConfiguration.Property(a => a.State)
                .HasMaxLength(60);

            addressConfiguration.Property(a => a.Country)
                .HasMaxLength(90)
                .IsRequired();

            addressConfiguration.Property(a => a.City)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}