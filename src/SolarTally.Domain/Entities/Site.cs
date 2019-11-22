using System;
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
    public class Site : BaseEntity<int>, IAggregateRoot, IReadOnlySiteSettings
    {
        public string Name { get; set; }

        public Address MainAddress { get; set; }

        public decimal NumSolarHours { get; private set; }
        
        public TimeInterval PeakSolarInterval { get; private set; }

        // Consumption Nav prop
        public Consumption Consumption { get; private set; }

        public Site(string name)
        {   
            Name = name;

            // Add a new Consumption profile. This will not add any child
            // ApplianceUsages by default.
            Consumption = new Consumption(this);

            this.SetPeakSolarInterval(GetDefaultTimeInterval());
        }

        public void SetPeakSolarInterval(TimeInterval peakSolarInterval)
        {
            PeakSolarInterval = peakSolarInterval;
            foreach(var applianceUsage in Consumption.ApplianceUsages)
            {
                // Restrict solar intervals to lie within peakSolarInterval
                applianceUsage.HandleSolarIntervalUpdated();
            }
            
            NumSolarHours = (decimal) PeakSolarInterval.Difference.TotalHours;
        }

        private TimeInterval GetDefaultTimeInterval() => new TimeInterval(
            8,0,16,0
        );
    }
}