using System;
using SolarTally.Domain.Common;
using Ardalis.GuardClauses;
using System.Collections.Generic;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Interfaces;
using SolarTally.Domain.Enumerations;
using SolarTally.Domain.Exceptions;

namespace SolarTally.Domain.Entities
{
    /// <summary>
    /// Daily usage pattern (in time) of an appliance.
    /// </summary>
    public class ApplianceUsageSchedule : IApplianceUsageSchedule
    {
        public int ApplianceUsageId { get; private set; }
        public ApplianceUsage ApplianceUsage { get; private set; }

        private readonly List<UsageTimeInterval> _usageIntervals;
        public IReadOnlyCollection<UsageTimeInterval> UsageIntervals
            => _usageIntervals;

        public IReadOnlySiteSettings ReadOnlySiteSettings { get; private set; }

        private TimeSpan _totalTimeOnSolar;
        public TimeSpan TotalTimeOnSolar => _totalTimeOnSolar;

        private TimeSpan _totalTimeOffSolar;
        public TimeSpan TotalTimeOffSolar => _totalTimeOffSolar;

        private ApplianceUsageSchedule()
        /// <summary>
        /// Number of hrs of solar to run the appliance
        /// (must be < SiteNumSolarHours)
        /// </summary>
        public decimal HoursOnSolar { get; private set; }

        /// <summary>
        /// Number of hrs to run the appliance on backup
        /// (NumHoursOnSolar + NumHoursOnBackup < 24)
        /// </summary>
        public decimal HoursOffSolar { get; private set; }

        /// <summary>
        /// Hrs to run the appliance.
        /// </summary>
        public decimal Hours { get; private set; }

        {
            // Needed for EF core. Fcuk.
        }

        public ApplianceUsageSchedule(
            IReadOnlySiteSettings readOnlySiteSettings)
        {
            ReadOnlySiteSettings = readOnlySiteSettings;
            _usageIntervals = new List<UsageTimeInterval>();
            RecalculateTotalTimes();
        }

        protected override void _ClearUsageIntervals()
        {
            _usageIntervals.Clear();
        }

        protected override void _AddUsageInterval(
            int startHr, int startMin, int endHr, int endMin,
            UsageKind usageKind
        )
        {
            var ti = new TimeInterval(startHr, startMin, endHr, endMin);
            
            // If we're trying to add a UsageInterval that's UsingSolar
            if (usageKind == UsageKind.UsingSolar)
            {
                // but it starts before the PeakSolarInterval start
                // or ends after the PeakSolarInterval end
                if (ti.Start < 
                    ReadOnlySiteSettings.PeakSolarInterval.Start ||
                    ti.End >
                    ReadOnlySiteSettings.PeakSolarInterval.End)
                {
                    // bad
                    throw new TimeIntervalArgumentInvalidException("When adding a UsageTimeInterval that is UsingSolar, the start/end cannot be before/after the Site's PeakSolarInterval start/end, respectively.");
                }
            }

            var newUTI = new UsageTimeInterval(ti, usageKind);
            
            for (int i = 0; i < _usageIntervals.Count; ++i)
            {
                var currTI = _usageIntervals[i].TimeInterval;

                // So now we know that either
                // * We have a UsingSolar interval but it's within the 
                //   PeakSolarInterval, or
                // * We have an arbitrarily shaped Non-UsingSolar interval

                // If our new time interval starts before the current interval,
                if (ti.Start < currTI.Start)
                {
                    // and it also ends before the current interval
                    if (ti.End <= currTI.Start)
                    {
                        // Just insert it and return
                        _usageIntervals.Insert(i, newUTI);
                        return;
                    }
                    else // it ends after the current interval
                    {
                        throw new TimeIntervalArgumentInvalidException("It is not possible to add a new UsageTimeInterval that overlaps with an existing UsageTimeInterval.");
                    }
                }
                // Else if our new time intervals starts in the middle of an
                // existing interval
                else if (ti.Start < currTI.End)
                {
                    // bad
                    throw new TimeIntervalArgumentInvalidException("It is not possible to add a new UsageTimeInterval that overlaps with an existing UsageTimeInterval.");
                }
            }
            // If we're here then we're past all the intervals. So OK to Add.
            _usageIntervals.Add(newUTI);
        }
        
        protected override void _HandlePeakSolarIntervalUpdated()
        {
            var ti = ReadOnlySiteSettings.PeakSolarInterval;
            int startHr = ti.Start.Hours, startMin = ti.Start.Minutes;
            int endHr   = ti.End.Hours,   endMin   = ti.End.Minutes;

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
                        // Make sure to update uTI in case the same interval is
                        // also a candidate for upper bound trimming.
                        uTI = newTI;
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
    
        public override void RecalculateTotalTimes()
        {
            _totalTimeOnSolar = new TimeSpan(0,0,0);
            _totalTimeOffSolar = new TimeSpan(0,0,0);

            foreach(var ui in _usageIntervals)
            {
                if (ui.UsageKind == UsageKind.UsingSolar)
                {
                    _totalTimeOnSolar += ui.TimeInterval.Difference;
                }
                else
                {
                    _totalTimeOffSolar += ui.TimeInterval.Difference;
                }
            }

            HoursOnSolar = (decimal) _totalTimeOnSolar.TotalHours;
            HoursOffSolar = (decimal) _totalTimeOffSolar.TotalHours;
            Hours = HoursOnSolar + HoursOffSolar;
        }
    }
}