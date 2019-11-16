using System;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.Domain.Enumerations;
using SolarTally.Domain.Exceptions;
using SolarTally.Domain.ValueObjects;
using System.Linq;

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
    }
}