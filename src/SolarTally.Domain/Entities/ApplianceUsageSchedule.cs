using SolarTally.Domain.Common;
using Ardalis.GuardClauses;
using System.Collections.Generic;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Interfaces;
using SolarTally.Domain.Enumerations;

namespace SolarTally.Domain.Entities
{
    /// <summary>
    /// Daily usage pattern (in time) of an appliance.
    /// </summary>
    public class ApplianceUsageSchedule : BaseEntity<int>
    {
        public int ApplianceUsageId { get; private set; }
        public ApplianceUsage ApplianceUsage { get; private set; }

        private readonly List<UsageTimeInterval> _usageTimeIntervals;
        public IReadOnlyCollection<UsageTimeInterval> UsageTimeIntervals
            => _usageTimeIntervals;

        public ApplianceUsageSchedule()
        {
            _usageTimeIntervals = new List<UsageTimeInterval>();
        }

        public void SetPeakSolarInterval(TimeInterval ti)
        {
            var newTI = new TimeInterval(ti.Start.Hours, ti.Start.Minutes,
                ti.End.Hours, ti.End.Minutes);
            var uTI = new UsageTimeInterval(newTI, UsageKind.UsingSolar);
            _usageTimeIntervals.Add(uTI);
        }
    }
}