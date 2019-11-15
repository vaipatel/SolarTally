using System;
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
        [InlineData(8,0,16,0,8,0,16,0)]
        public void ShouldThrowForIntervalsOutsidePeakSolarInterval(
            int peakStartHr, int peakStartMin, int peakEndHr, int peakEndMin,
            int startHr, int startMin, int endHr, int endMin)
        {
            var aus =
                new ApplianceUsageSchedule(
                    new MockReadOnlySiteSettings(
                        peakStartHr, peakStartMin, peakEndHr, peakEndMin
                    )
                );
            Assert.Throws<TimeIntervalArgumentInvalidException>(() =>
                aus.AddUsageInterval(startHr, startMin, endHr, endMin,
                    UsageKind.UsingSolar)
            );
        }
    }
}