using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarTally.Domain.Entities;

namespace SolarTally.Persistence.Configurations
{
    public class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.Property(a => a.Name)
                .HasMaxLength(50)
                .IsRequired();
            
            builder.OwnsOne(a => a.MainAddress);

            builder.Property(a => a.NumSolarHours)
                .IsRequired();
        }
    }
}