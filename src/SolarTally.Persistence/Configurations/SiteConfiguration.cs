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
                .HasDefaultValue("Default Site Name")
                .IsRequired();
            
            siteConfiguration.OwnsOne(s => s.MainAddress);

            siteConfiguration.Property(s => s.NumSolarHours)
                .HasColumnType("smallint")
                .HasDefaultValue<int>(8)
                .IsRequired();

            // Note: If I don't configure the HasForeignKey<Consumption>(), then
            // I'll get the following error
            // -------
            // System.AggregateException : One or more errors occurred. (The 
            // child/dependent side could not be determined for the one-to-one 
            // relationship between 'Consumption.Site' and 'Site.Consumption'. 
            // To identify the child/dependent side of the relationship, 
            // configure the foreign key property. If these navigations should 
            // not be part of the same relationship configure them without 
            // specifying the inverse. 
            // See http://go.microsoft.com/fwlink/?LinkId=724062 for more 
            // details.)
            // -------
            // Note (cont.): Basically EF Core is confused about which is the
            // child in the one-to-one relationship between Site and 
            // Consumption. It will resolve this confusion when it can detect a
            // FK configuration, as we have done with the HasForeignKey() call
            // below.
            // See the link in the error.
            // Also see the question https://stackoverflow.com/q/51808912 to
            // understand which one is dubbed the child by EF Core.

            siteConfiguration.HasOne(s => s.Consumption)
                .WithOne(c => c.Site)
                .HasForeignKey<Consumption>(c => c.SiteId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);
        }
    }
}