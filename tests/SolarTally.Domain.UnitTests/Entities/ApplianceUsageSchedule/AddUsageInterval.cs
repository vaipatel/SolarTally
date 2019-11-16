using System.Collections.Generic;
using System.Linq;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.Domain.Enumerations;
using SolarTally.Domain.Exceptions;
using SolarTally.Domain.ValueObjects;

namespace SolarTally.Domain.UnitTests.Entities
{
    public class ApplianceUsageSchedule_AddUsageInterval
    {
        [Theory]
        // Start before Peak's Start
        [InlineData(8,0,16,0,7,0,15,0)]
        // End after Peak's End
        [InlineData(8,0,16,0,9,0,17,0)]
        public void ShouldThrowForSolarIntervalOutsidePeak(
            int peakStartHr, int peakStartMin, int peakEndHr, int peakEndMin,
            int startHr, int startMin, int endHr, int endMin)
        {
            var aus =
                new ApplianceUsageSchedule(
                    new MockReadOnlySiteSettings(
                        peakStartHr, peakStartMin, peakEndHr, peakEndMin
                    )
                );
            Assert.Throws<TimeIntervalArgumentInvalidException>(() => {
                aus.AddUsageInterval(startHr, startMin, endHr, endMin,
                    UsageKind.UsingSolar);
            });
        }

        [Theory]
        // Start/end same as Peak's
        [InlineData(8,0,16,0,8,0,16,0,UsageKind.UsingSolar)]
        // All within Peak
        [InlineData(8,0,16,0,8,1,15,59,UsageKind.UsingSolar)]
        // Non-UsingSolar (UsingBattery) same as Peak
        [InlineData(8,0,16,0,8,0,16,0,UsageKind.UsingBattery)]
        // Non-UsingSolar (UsingBattery) within Peak
        [InlineData(8,0,16,0,8,1,15,59,UsageKind.UsingBattery)]
        // Non-UsingSolar (UsingBattery) before Peak's Start
        [InlineData(8,0,16,0,7,0,7,59,UsageKind.UsingBattery)]
        // Non-UsingSolar (UsingBattery) after Peaks' End
        [InlineData(8,0,16,0,16,1,17,0,UsageKind.UsingBattery)]
        public void ShouldAddUsageIntervalToEmpty(
            int peakStartHr, int peakStartMin, int peakEndHr, int peakEndMin,
            int startHr, int startMin, int endHr, int endMin,
            UsageKind usageKind)
        {
            var aus =
                new ApplianceUsageSchedule(
                    new MockReadOnlySiteSettings(
                        peakStartHr, peakStartMin, peakEndHr, peakEndMin
                    )
                );
            aus.AddUsageInterval(startHr, startMin, endHr, endMin, usageKind);
            Assert.Single(aus.UsageIntervals);
            Assert.Equal(new UsageTimeInterval(
                new TimeInterval(startHr, startMin, endHr, endMin), usageKind),
                aus.UsageIntervals.First());
        }

        [Theory]
        [ClassData(typeof(OverlapThrowData))]
        public void ShouldThrowForOverlappingInterval(
            int peakStartHr, int peakStartMin, int peakEndHr, int peakEndMin, List<UsageTimeInterval> usageIntervals, UsageTimeInterval uTI)
        {
            var aus =
                new ApplianceUsageSchedule(
                    new MockReadOnlySiteSettings(
                        peakStartHr, peakStartMin, peakEndHr, peakEndMin
                    )
                );
            
            foreach(var u in usageIntervals)
            {
                var ti = u.TimeInterval;
                aus.AddUsageInterval(
                    ti.Start.Hours, ti.Start.Minutes,
                    ti.End.Hours, ti.End.Minutes,
                    u.UsageKind);
            }

            Assert.Throws<TimeIntervalArgumentInvalidException>(() => {
                var ti = uTI.TimeInterval;
                aus.AddUsageInterval(
                    ti.Start.Hours, ti.Start.Minutes,
                    ti.End.Hours, ti.End.Minutes,
                    uTI.UsageKind);
            });
        }

        public class OverlapThrowData : TheoryData<int, int, int, int, List<UsageTimeInterval>, UsageTimeInterval>
        {
            public OverlapThrowData()
            {
                // Overlaps with first and only solar interval
                Add(
                    8,0,16,0,
                    new List<UsageTimeInterval>() {
                        new UsageTimeInterval(new TimeInterval(08,00,12,00))
                    },
                    new UsageTimeInterval(new TimeInterval(11,59,16,00))
                );
                // Overlaps with first and only non-solar interval
                Add(
                    8,0,16,0,
                    new List<UsageTimeInterval>() {
                        new UsageTimeInterval(new TimeInterval(08,00,12,00),
                            UsageKind.UsingBattery)
                    },
                    new UsageTimeInterval(new TimeInterval(11,59,16,00))
                );
                // Overlaps with second solar interval (back-to-back)
                Add(
                    8,0,16,0,
                    new List<UsageTimeInterval>() {
                        new UsageTimeInterval(new TimeInterval(08,00,12,00)),
                        new UsageTimeInterval(new TimeInterval(12,00,16,00))
                    },
                    new UsageTimeInterval(new TimeInterval(12,01,16,00))
                );
                // Overlaps with second solar interval (not back-to-back)
                Add(
                    8,0,16,0,
                    new List<UsageTimeInterval>() {
                        new UsageTimeInterval(new TimeInterval(08,00,11,00)),
                        new UsageTimeInterval(new TimeInterval(13,00,16,00))
                    },
                    new UsageTimeInterval(new TimeInterval(12,00,16,00))
                );
                // Overlaps with non-solar interval after peak
                Add(
                    8,0,16,0,
                    new List<UsageTimeInterval>() {
                        new UsageTimeInterval(new TimeInterval(06,00,08,00),
                            UsageKind.UsingBattery),
                        new UsageTimeInterval(new TimeInterval(08,00,12,00)),
                        new UsageTimeInterval(new TimeInterval(12,00,16,00),
                            UsageKind.UsingMains),
                        new UsageTimeInterval(new TimeInterval(17,00,21,00),
                            UsageKind.UsingBattery)
                    },
                    new UsageTimeInterval(new TimeInterval(18,00,20,00),
                        UsageKind.UsingMains)
                );
                // Overlaps with multiple intervals
                Add(
                    8,0,16,0,
                    new List<UsageTimeInterval>() {
                        new UsageTimeInterval(new TimeInterval(06,00,08,00),
                            UsageKind.UsingBattery),
                        new UsageTimeInterval(new TimeInterval(10,00,12,00)),
                        new UsageTimeInterval(new TimeInterval(12,00,16,00),
                            UsageKind.UsingMains),
                        new UsageTimeInterval(new TimeInterval(17,00,21,00),
                            UsageKind.UsingBattery)
                    },
                    new UsageTimeInterval(new TimeInterval(08,00,20,00),
                        UsageKind.UsingMains)
                );
            }
        }
    }
}