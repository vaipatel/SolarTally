using SolarTally.Domain.Common;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Interfaces;
using SolarTally.Domain.Events;
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

        public int NumSolarHours { get; private set; }

        // Consumption Nav prop
        public Consumption Consumption { get; private set; }

        public Site(string name, int numSolarHours)
        {   
            Name = name;
            Consumption = new Consumption(this);
            
            this.SetNumSolarHours(numSolarHours);
        }

        public void SetNumSolarHours(int numSolarHours)
        {
            Guard.Against.OutOfRange(numSolarHours, nameof(numSolarHours),
                0, 24);
            NumSolarHours = numSolarHours;

            // Cap appliance usage hours if needed
            foreach(var applianceUsage in Consumption.ApplianceUsages)
            {
                if (applianceUsage.NumHoursOnSolar > numSolarHours)
                {
                    applianceUsage.SetNumHoursOnSolar(numSolarHours);
                }
            }
        }
    }
}