using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarTally.Domain.Entities;

namespace SolarTally.Persistence.Configurations
{
    public class ApplianceUsageScheduleConfiguration :
        IEntityTypeConfiguration<ApplianceUsageSchedule>
    {
        public void Configure(
            EntityTypeBuilder<ApplianceUsageSchedule> ausConfiguration)
        {
            ausConfiguration.Ignore(b => b.DomainEvents);
            
            ausConfiguration.OwnsMany(aus => aus.TimeIntervalsWithKind,
            tiwkConfig => {
                // Stole the bottom 3 lines from https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities#collections-of-owned-types
                // tiwkConfig.WithOwner().HasForeignKey("ApplianceUsageId");
                // tiwkConfig.Property<int>("Id");
                // tiwkConfig.HasKey("Id");

                tiwkConfig.OwnsOne(tiwk => tiwk.TimeInterval, tiConfig => {
                    tiConfig.Property(ti => ti.Start).HasColumnType("time(0)");
                    tiConfig.Property(ti => ti.End).HasColumnType("time(0)");
                    tiConfig.Property(ti => ti.Difference)
                        .HasColumnType("time(0)");
                });

                tiwkConfig.ToTable("TimeIntervalsWithKind");
            });
        }
    }
}