using SolarTally.Domain.Common;
using Ardalis.GuardClauses;

namespace SolarTally.Domain.Entities
{
    /// <summary>
    /// An electricity-consuming device or equipment.
    /// </summary>
    public class Appliance : BaseEntity<int>
    {
        public string Name { get; private set; }

        public string Description { get; set; }

        //public byte[] Image { get; set; }

        /// <summary>The amount of power consumed in Watts.</summary>
        public decimal DefaultPowerConsumption { get; private set; }

        /// <summary>Power consumed at startup in Watts.</summary>
        /// <remarks>
        /// This can be quite a bit more than the "regular" power consumed.
        /// </remarks>
        public decimal DefaultStartupPowerConsumption { get; private set; }

        private Appliance()
        {
            // Apparently required by EF
        }

        public Appliance(string name, string description,
            decimal defaultPowerConsumption,
            decimal defaultStartupPowerConsumption)
        {
            this.SetName(name);
            this.SetDefaultPowerConsumption(defaultPowerConsumption);
            this.SetDefaultStartupPowerConsumption(
                defaultStartupPowerConsumption);
            Description = description;
        }

        public void SetName(string name)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Name = name;
        }

        public void SetDefaultPowerConsumption(decimal defaultPowerConsumption)
        {
            Guard.Against.LessThan(defaultPowerConsumption,
                nameof(defaultPowerConsumption), 0);   
            DefaultPowerConsumption = defaultPowerConsumption;
        }

        public void SetDefaultStartupPowerConsumption(
            decimal defaultStartupPowerConsumption)
        {
            Guard.Against.LessThan(defaultStartupPowerConsumption,
                nameof(defaultStartupPowerConsumption), 0);
            DefaultStartupPowerConsumption = defaultStartupPowerConsumption;
        }
    }
}