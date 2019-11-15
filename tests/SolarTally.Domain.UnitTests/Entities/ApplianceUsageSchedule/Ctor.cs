using System;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.Domain.Exceptions;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Interfaces;

namespace SolarTally.Domain.UnitTests.Entities
{
    public class ApplianceUsageSchedule_Ctor
    {   
        [Fact]
        public void ShouldHaveNoUsageIntervals()
        {
            var aus = new ApplianceUsageSchedule(new MockReadOnlySiteSettings());
            Assert.Empty(aus.UsageIntervals);
        }
    }
}