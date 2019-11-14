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

        private readonly List<TimeIntervalWithKind> _timeIntervalsWithKind;
        public IReadOnlyCollection<TimeIntervalWithKind> TimeIntervalsWithKind
            => _timeIntervalsWithKind;

        public ApplianceUsageSchedule()
        {
            _timeIntervalsWithKind = new List<TimeIntervalWithKind>();
        }

        public void SetPeakSolarInterval(TimeInterval ti)
        {
            var t = new TimeInterval(ti.Start.Hours, ti.Start.Minutes,
                ti.End.Hours, ti.End.Minutes);
            var tiwk = new TimeIntervalWithKind(t, TimeIntervalKind.UsingSolar);
            _timeIntervalsWithKind.Add(tiwk);
        }
    }
}