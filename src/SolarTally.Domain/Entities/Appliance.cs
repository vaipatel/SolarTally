using SolarTally.Domain.Common;

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

        public decimal DefaultPowerConsumption { get; set; }
    }
}