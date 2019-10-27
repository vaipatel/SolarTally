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

            builder.Property(s => s.NumSolarHours)
                .HasColumnType("smallint")
                .IsRequired();

            // Don't think this is needed as it shud be autoconfigured by efcore
            // builder.HasOne(s => s.Consumption)
            //     .WithOne(c => c.Site)
            //     .HasForeignKey<Consumption>(c => c.SiteId);
        }
    }
}