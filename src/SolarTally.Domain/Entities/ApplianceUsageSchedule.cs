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

        private readonly List<UsageTimeInterval> _usageIntervals;
        public IReadOnlyCollection<UsageTimeInterval> UsageIntervals
            => _usageIntervals;

        public ApplianceUsageSchedule()
        {
            _usageIntervals = new List<UsageTimeInterval>();
        }

        public void SetPeakSolarInterval(
            TimeInterval ti, bool addIfEmpty = false
        )
        {
            int startHr = ti.Start.Hours, startMin = ti.Start.Minutes;
            int endHr   = ti.End.Hours,   endMin   = ti.End.Minutes;

            if (addIfEmpty)
            {
                if (_usageIntervals.Count == 0)
                {
                    var newTI = new TimeInterval(startHr, startMin, endHr, endMin);
                    var uTI = new UsageTimeInterval(newTI, UsageKind.UsingSolar);
                    _usageIntervals.Add(uTI);
                    return;
                }
            }

            bool lowerDone = false;
            bool upperDone = false;
            for(int i = 0; i < _usageIntervals.Count; ++i)
            {
                var uTI = _usageIntervals[i].TimeInterval;
                var usage = _usageIntervals[i].UsageKind;

                // Ensure we're just dealing with solar usage intervals.
                if (usage != UsageKind.UsingSolar)
                {
                    continue;
                }

                // If we have a solar usage interval that is completely outside
                // the new peak solar interval, then convert it to mains usage.
                if (uTI.End < ti.Start || uTI.Start > ti.End)
                {
                    var newTI = new TimeInterval(
                        uTI.Start.Hours, uTI.Start.Minutes,
                        uTI.End.Hours, uTI.End.Minutes);
                    _usageIntervals[i] = new UsageTimeInterval(newTI,
                        UsageKind.UsingMains);
                    continue;
                }

                // If we haven't trimmed the lower bound yet ..
                if (!lowerDone)
                {
                    // .. and we have a solar usage interval starting before
                    // the start of the peak solar interval
                    if (uTI.Start < ti.Start)
                    {
                        // we gottu trim it!
                        var newTI = new TimeInterval(
                            startHr, startMin, uTI.End.Hours, uTI.End.Minutes
                        );
                        _usageIntervals[i] = new UsageTimeInterval(newTI);
                        // since we're storing intervals in temporal order,
                        // we won't need to do this again.
                        lowerDone = true;
                    }
                }

                // If we haven't trimmed the upper bound yet ..
                if (!upperDone)
                {
                    // .. and we have a solar usage interval starting after
                    // the end of the peak solar interval
                    if (uTI.End > ti.End)
                    {
                        // we gottu trim it!
                        var newTI = new TimeInterval(
                            uTI.Start.Hours, uTI.Start.Minutes, endHr, endMin
                        );
                        _usageIntervals[i] = new UsageTimeInterval(newTI);
                        // since we're storing intervals in temporal order,
                        // we won't need to do this again.
                        upperDone = true;
                    }
                }
            }
        }
    }
}