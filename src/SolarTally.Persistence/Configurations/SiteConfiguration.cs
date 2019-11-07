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

            // Previous (summarized)
            // --------------------------------------------------------------
            // I needed to configure HasForeignKey<Consumption>(c => c.SiteId)
            // here to let EF Core know which one is the dependant.
            // --------------------------------------------------------------

            // Edit
            // --------------------------------------------------------------
            // When trying to share the Sites and Consumptions tables, I got
            // the [IncompatibleTableNoRelationship](https://github.com/aspnet/EntityFrameworkCore/blob/0d76bbf45a42148924b413ef8f37bf49c1ce10d3/src/EFCore.Relational/Properties/RelationalStrings.resx#L290-L292)
            // error. So the two tables weren't really connected.
            // Anyway, I think this is because of the PK Id conventions.
            // I've borrowed below from an [EF Core sample](https://github.com/aspnet/EntityFramework.Docs/blob/master/samples/core/Modeling/TableSplitting/TableSplittingContext.cs#L27-L28)
            // with Id conventions similar to mine.
            // --------------------------------------------------------------

            siteConfiguration.HasOne(s => s.Consumption)
                .WithOne(c => c.Site)
                .HasForeignKey<Consumption>(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);
        }
    }
}