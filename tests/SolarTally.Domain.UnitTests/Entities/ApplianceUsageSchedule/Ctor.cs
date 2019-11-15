using System;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.Domain.Exceptions;
using SolarTally.Domain.ValueObjects;

namespace SolarTally.Domain.UnitTests.Entities
{
    public class ApplianceUsageSchedule_Ctor
    {
        [Fact]
        public void ShouldHaveNoUsageIntervals()
        {
            var aus = new ApplianceUsageSchedule();
            Assert.Empty(aus.UsageIntervals);
        }
    }
}