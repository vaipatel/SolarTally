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
        // Some constants to reduce magic numbers
        public const int DefaultQuantity = 1;
        public const int DefaultPercentHrsOnSolar = 1;

        public int ApplianceId { get; private set; }
        public Appliance Appliance { get; private set; }

        /// <summary>The number of appliances owned.</summary>
        public int Quantity { get; private set; }

        /// <summary>The amount of power consumed in Watts.</summary>
        public decimal PowerConsumption { get; private set; }

        /// <summary>
        /// Hrs to run the appliance.
        /// </summary>
        /// <remark>
        /// TODO: Calculate this from Noda LocalTime or similar to make it
        /// easier for the user to input this info.
        /// </remark>
        public int NumHours { get; private set; }

        /// <summary>
        /// Percent of hrs to run the appliance when solar is available.
        /// </summary>
        public decimal PercentHrsOnSolar { get; private set; }

        public bool Enabled { get; private set; }

        private ApplianceUsage()
        {
            // Apparently required by EF Core
        }

        public ApplianceUsage(Appliance appliance, int quantity,
            decimal powerConsumption, int numHours, decimal percentHrsOnSolar,
            bool enabled)
        {
            Guard.Against.Null(appliance, nameof(appliance));
            Guard.Against.LessThan(quantity, nameof(quantity), 0);
            Guard.Against.LessThan(powerConsumption,
                nameof(powerConsumption), 0);
            Guard.Against.OutOfRange(numHours, 
                nameof(numHours), 0, 24);
            Guard.Against.CustomOutOfRange(percentHrsOnSolar,
                nameof(percentHrsOnSolar), 0, 1);

            ApplianceId = appliance.Id;
            Appliance = appliance;
            Quantity = quantity;
            PowerConsumption = powerConsumption;
            NumHours = numHours;
            PercentHrsOnSolar = percentHrsOnSolar;
            Enabled = enabled;
        }

        public void SetNumHours(int numHours)
        {
            Guard.Against.OutOfRange(numHours, 
                nameof(numHours), 0, 24);
            NumHours = numHours;
        }
        }

        public void SetPowerConsumptionToDefault()
        {
            PowerConsumption = Appliance.DefaultPowerConsumption;
        }

        public void SetAppliance(Appliance appliance)
        {
            Guard.Against.Null(appliance, nameof(appliance));
            ApplianceId = appliance.Id;
            Appliance = appliance;
        }
    }
}