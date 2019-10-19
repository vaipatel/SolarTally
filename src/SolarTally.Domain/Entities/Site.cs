using SolarTally.Domain.Common;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Interfaces;

namespace SolarTally.Domain.Entities
{
    /// <summary>
    /// A site encapsulates the consumption and costing estimates at a location
    /// </summary>
    public class Site : BaseEntity<int>, IAggregateRoot
    {
        public string Name { get; set; }

        public Address MainAddress { get; set; }

        // Consumption Foreign Key
        public int ConsumptionId { get; set; }
        // Consumption Nav prop
        public Consumption Consumption { get; set; }
    }
}