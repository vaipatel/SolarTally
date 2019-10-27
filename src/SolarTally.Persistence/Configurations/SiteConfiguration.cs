using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarTally.Domain.Entities;

namespace SolarTally.Persistence.Configurations
{
    public class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> siteConfiguration)
        {
            siteConfiguration.Ignore(b => b.DomainEvents);
            
            siteConfiguration.Property(s => s.Name)
                .HasMaxLength(50)
                .IsRequired();
            
            siteConfiguration.OwnsOne(s => s.MainAddress);

            siteConfiguration.Property(s => s.NumSolarHours)
                .HasColumnType("smallint")
                .IsRequired();

            // Don't think this is needed as it shud be autoconfigured by efcore
            // siteConfiguration.HasOne(s => s.Consumption)
            //     .WithOne(c => c.Site)
            //     .HasForeignKey<Consumption>(c => c.SiteId);
        }
    }
}