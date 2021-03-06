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

            ausConfiguration.Ignore(aus => aus.ReadOnlySiteSettings);

            ausConfiguration.Property(aus => aus.TotalTimeOnSolar)
                .HasColumnType("time(0)");
            
            ausConfiguration.Property(aus => aus.TotalTimeOffSolar)
                .HasColumnType("time(0)");
            
            
            ausConfiguration.Property(aus => aus.HoursOnSolar)
                .HasColumnType("decimal(4,2)")
                .IsRequired();
            
            ausConfiguration.Property(aus => aus.HoursOffSolar)
                .HasColumnType("decimal(4,2)")
                .IsRequired();
            
            ausConfiguration.Property(aus => aus.Hours)
                .HasColumnType("decimal(4,2)")
                .IsRequired();
            
            ausConfiguration.OwnsMany(aus => aus.UsageIntervals,
            utiConfig => {
                // Stole the bottom 3 lines from https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities#collections-of-owned-types
                utiConfig.WithOwner().HasForeignKey("ApplianceUsageScheduleId");
                utiConfig.Property<int>("Id");
                utiConfig.HasKey("Id");

                utiConfig.OwnsOne(uti => uti.TimeInterval, tiConfig => {
                    tiConfig.Property(ti => ti.Start).HasColumnType("time(0)");
                    tiConfig.Property(ti => ti.End).HasColumnType("time(0)");
                    tiConfig.Property(ti => ti.Difference)
                        .HasColumnType("time(0)");
                });

                utiConfig.ToTable("UsageIntervals");
            });
        }
    }
}