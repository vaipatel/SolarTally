using SolarTally.Domain.Common;
using SolarTally.Domain.ValueObjects;
using Ardalis.GuardClauses;

namespace SolarTally.Domain.Entities
{
    /// <summary>
    /// Encapsulates the particular usage of an Appliance for a given Consumer.
    /// </summary>
    /// <remark> 
    /// I can't decide if ApplianceUsage has an identity in the domain outside 
    /// of Consumption. It sure seems like it doesn't. It seems much more like 
    /// an Address than an Order.
    /// 
    /// Anyway, maybe it's being unique to a customer/user is not reason enough 
    /// to make it a ValueObject. For example, an Order is also mostly unique to
    /// a customer/user.
    /// 
    /// Furthermore, I gotta think - should ApplianceUsage be immutable? Doesn't
    /// seem right to me.
    /// 
    /// I'm making it an Entity for now. We shall see.
    /// </remark>
    public class ApplianceUsage : BaseEntity<int>
    {
        public int ApplianceId { get; private set; }
        public Appliance Appliance { get; private set; }

        public int ConsumptionId { get; private set; }

        public UsageParams Usage { get; private set; }

        public bool Enabled { get; private set; }

        private ApplianceUsage()
        {
            // Apparently required by EF Core
        }

        public ApplianceUsage(Appliance appliance, UsageParams usage, 
            bool enabled)
        {
            Guard.Against.Null(appliance, nameof(appliance));
            Guard.Against.Null(usage, nameof(usage));

            ApplianceId = appliance.Id;
            Appliance = appliance;
            Usage = usage;
            Enabled = enabled;
        }

        public void SetAppliance(Appliance appliance)
        {
            Guard.Against.Null(appliance, nameof(appliance));
            ApplianceId = appliance.Id;
            Appliance = appliance;
        }
    }
}