using System.Linq;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.Domain.Exceptions;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Interfaces;

namespace SolarTally.Domain.UnitTests.Entities
{
    public class ApplianceUsageSchedule_SetPeakSolarInterval
    {
        public class MockReadOnlySiteSettings : IReadOnlySiteSettings
        {
            private TimeInterval _peakSolarInterval;
            public string Name => "A Site Name";
            public int NumSolarHours => 8;
            public TimeInterval PeakSolarInterval => _peakSolarInterval;

            public MockReadOnlySiteSettings()
            {
                _peakSolarInterval = new TimeInterval(8,0,16,0);
            }
        }
        
        [Theory]
        // New PeakSolarInterval completely outside old, nothing should happen
        [InlineData(8,0,16,0,7,0,17,0,8,0,16,0)]
        // New PeakSolarInterval start outside start, start should not change
        [InlineData(8,0,16,0,7,0,15,0,8,0,15,0)]
        // New PeakSolarInterval end outside old, end should not change
        [InlineData(8,0,16,0,9,0,17,0,9,0,16,0)]
        // New PeakSolarInterval completely inside old, should trim start n end
        [InlineData(8,0,16,0,9,0,15,0,9,0,15,0)]
        public void ShouldNotAddIfNotEmptyButInsteadTrimWhenNeeded(
            int startHr1, int startMin1, int endHr1, int endMin1,
            int startHr2, int startMin2, int endHr2, int endMin2,
            int startHrExp, int startMinExp, int endHrExp, int endMinExp)
        {
            var aus = new ApplianceUsageSchedule(new MockReadOnlySiteSettings());
            var ti = new TimeInterval(startHr1,startMin1,endHr1,endMin1);
            var ti2 = new TimeInterval(startHr2,startMin2,endHr2,endMin2);
            var tiExp = new TimeInterval(startHrExp,startMinExp,
                endHrExp,endMinExp);
            aus.SetPeakSolarInterval(ti, addIfEmpty: true);
            aus.SetPeakSolarInterval(ti2, addIfEmpty: true);
            Assert.Single(aus.UsageIntervals);
            Assert.Equal(tiExp, aus.UsageIntervals.First().TimeInterval);
            // TODO: Also test with AddUsageInterval() once complete.
        }

        [Fact]
        public void ShouldAddIfEmpty()
        {
            var aus = new ApplianceUsageSchedule(new MockReadOnlySiteSettings());
            var ti = new TimeInterval(8,0,16,0);
            aus.SetPeakSolarInterval(ti, addIfEmpty: true);
            Assert.Single(aus.UsageIntervals);
            Assert.Equal(ti, aus.UsageIntervals.First().TimeInterval);
        }
    }
}