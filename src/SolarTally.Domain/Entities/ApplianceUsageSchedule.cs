using SolarTally.Domain.Common;
using Ardalis.GuardClauses;
using System.Collections.Generic;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Interfaces;

namespace SolarTally.Domain.Entities
{
    /// <summary>
    /// Daily usage pattern (in time) of an appliance.
    /// </summary>
    public class ApplianceUsageSchedule : BaseEntity<int>
    {
        public int ApplianceUsageId { get; private set; }
        public ApplianceUsage ApplianceUsage { get; private set; }

        private readonly List<TimeInterval> _timeIntervals;
        public IReadOnlyCollection<TimeInterval> TimeIntervals => _timeIntervals;

        private IConsumptionCalculator _consumptionCalculator { get; set; }

        private ApplianceUsageSchedule()
        {
        }

        public ApplianceUsageSchedule(
            IConsumptionCalculator consumptionCalculator)
        {
            _consumptionCalculator = consumptionCalculator;
        }
    }
}