using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarTally.Domain.ValueObjects;

namespace SolarTally.Persistence.Configurations
{
    /// <summary>
    /// Provides db configuration for the Address value object.
    /// </summary>
    /// <remarks>
    /// Edit: I thought we could configure Owned types (value objects), but I
    /// was wrong.
    /// See https://github.com/aspnet/EntityFrameworkCore/issues/15681.
    /// <a href="https://github.com/aspnet/EntityFrameworkCore/issues/15681#issuecomment-491320550">
    /// This comment by ajcvickers</a> tells us why we can't configure owned
    /// types. Read the rest to see proposals and current status.
    ///
    /// Previous:
    /// It seems that while we <a href="https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities#by-design-restrictions">
    /// cannot have a DbSet for a value object by design</a>, we <a href="https://github.com/dotnet-architecture/eShopOnWeb/blob/master/src/Infrastructure/Data/Config/AddressConfiguration.cs">
    /// can have a configuration</a> <a href="https://github.com/dotnet-architecture/eShopOnWeb/blob/master/src/Infrastructure/Data/CatalogContext.cs#L40">
    /// for the value object</a>. This makes sense to me because the
    /// configuration can offer info on the data types and constraints.
    /// </remarks>
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