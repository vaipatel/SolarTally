using System.Linq;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.Domain.Exceptions;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Interfaces;
using SolarTally.Domain.Enumerations;

namespace SolarTally.Domain.UnitTests.Entities
{
    public class ApplianceUsageSchedule_SetPeakSolarInterval
    {   
        [Theory]
        // New PeakSolarInterval completely outside old, nothing should happen
        [InlineData(8,0,16,0,7,0,17,0,8,0,16,0)]
        // New PeakSolarInterval start outside start, start should not change
        [InlineData(8,0,16,0,7,0,15,0,8,0,15,0)]
        // New PeakSolarInterval end outside old, end should not change
        [InlineData(8,0,16,0,9,0,17,0,9,0,16,0)]
        // New PeakSolarInterval completely inside old, should trim start n end
        [InlineData(8,0,16,0,9,0,15,0,9,0,15,0)]
        public void ShouldTrimSingleWhenNeeded(
            int startHr1, int startMin1, int endHr1, int endMin1,
            int startHr2, int startMin2, int endHr2, int endMin2,
            int startHrExp, int startMinExp, int endHrExp, int endMinExp)
        {
            var mockSite = new MockReadOnlySiteSettings();
            var aus = new ApplianceUsageSchedule(mockSite);
            var ti = new TimeInterval(startHr1,startMin1,endHr1,endMin1);
            var ti2 = new TimeInterval(startHr2,startMin2,endHr2,endMin2);
            var tiExp = new TimeInterval(startHrExp,startMinExp,
                endHrExp,endMinExp);
            aus.AddUsageInterval(startHr1, startMin1, endHr1, endMin1,
                UsageKind.UsingSolar);
            
            // HandlePeakSolarIntervalUpdated would be called inside
            // SetPeakSolarInterval in the actual Site. Here we call manually.
            mockSite.SetPeakSolarInterval(
                startHr2, startMin2, endHr2, endMin2
            );
            aus.HandlePeakSolarIntervalUpdated();
            
            // Check that there's still just the one UTI
            Assert.Single(aus.UsageIntervals);
            // Check that it's appropriately trimmed.
            Assert.Equal(tiExp, aus.UsageIntervals.First().TimeInterval);
        }

        [Fact]
        public void ShouldNotAddIfEmpty()
        {
            var mockSite = new MockReadOnlySiteSettings();
            var aus = new ApplianceUsageSchedule(mockSite);

            // HandlePeakSolarIntervalUpdated would be called inside
            // SetPeakSolarInterval in the actual Site. Here we call manually.
            mockSite.SetPeakSolarInterval(7,0,15,0);
            aus.HandlePeakSolarIntervalUpdated();
            
            // Check that there's still no UTI.
            Assert.Empty(aus.UsageIntervals);
        }
    }
}