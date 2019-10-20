using SolarTally.Domain.Common;
using Ardalis.GuardClauses;

namespace SolarTally.Domain.Entities
{
    /// <summary>
    /// An electricity-consuming device or equipment.
    /// </summary>
    public class Appliance : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        //public byte[] Image { get; set; }

        /// <summary>The amount of power consumed in Watts.</summary>
        public decimal DefaultPowerConsumption { get; set; }

        public int ApplianceUsageId { get; private set; }

        private Appliance()
        {
            // Apparently required by EF
        }

        public Appliance(string name, string description,
            decimal defaultPowerConsumption)
        {
            Guard.Against.LessThan(defaultPowerConsumption,
                nameof(defaultPowerConsumption), 0);
            Name = name;
            Description = description;
            DefaultPowerConsumption = defaultPowerConsumption;
        }
    }
}