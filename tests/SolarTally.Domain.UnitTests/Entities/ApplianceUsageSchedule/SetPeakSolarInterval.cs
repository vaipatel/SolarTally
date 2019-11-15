using System.Linq;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.Domain.Exceptions;
using SolarTally.Domain.ValueObjects;

namespace SolarTally.Domain.UnitTests.Entities
{
    public class ApplianceUsageSchedule_SetPeakSolarInterval
    {
        [Theory]
        [InlineData(8,0,16,0,9,0,15,0,9,0,15,0)]
        [InlineData(8,0,16,0,9,0,17,0,9,0,16,0)]
        public void ShouldNotAddIfNotEmptyButInsteadReset(
            int startHr1, int startMin1, int endHr1, int endMin1,
            int startHr2, int startMin2, int endHr2, int endMin2,
            int startHrExp, int startMinExp, int endHrExp, int endMinExp)
        {
            var aus = new ApplianceUsageSchedule();
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
            var aus = new ApplianceUsageSchedule();
            var ti = new TimeInterval(8,0,16,0);
            aus.SetPeakSolarInterval(ti, addIfEmpty: true);
            Assert.Single(aus.UsageIntervals);
            Assert.Equal(ti, aus.UsageIntervals.First().TimeInterval);
        }
    }
}