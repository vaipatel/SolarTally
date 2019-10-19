using SolarTally.Domain.Common;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Interfaces;
using Ardalis.GuardClauses;

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
        public int ConsumptionId { get; private set; }
        // Consumption Nav prop
        public Consumption Consumption { get; private set; }

        public Site(string name)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            
            Name = name;
            Consumption = new Consumption(this);
            ConsumptionId = Consumption.Id;
        }
    }
}