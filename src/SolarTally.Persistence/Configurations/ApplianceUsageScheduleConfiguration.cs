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
            
            ausConfiguration.OwnsMany(aus => aus.UsageTimeIntervals,
            utiConfig => {
                // Stole the bottom 3 lines from https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities#collections-of-owned-types
                // utiConfig.WithOwner().HasForeignKey("ApplianceUsageId");
                // utiConfig.Property<int>("Id");
                // utiConfig.HasKey("Id");

                utiConfig.OwnsOne(uti => uti.TimeInterval, tiConfig => {
                    tiConfig.Property(ti => ti.Start).HasColumnType("time(0)");
                    tiConfig.Property(ti => ti.End).HasColumnType("time(0)");
                    tiConfig.Property(ti => ti.Difference)
                        .HasColumnType("time(0)");
                });

                utiConfig.ToTable("UsageTimeIntervals");
            });
        }
    }
}