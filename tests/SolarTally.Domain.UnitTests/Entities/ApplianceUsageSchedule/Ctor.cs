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
        
        [Fact]
        public void ShouldHaveNoUsageIntervals()
        {
            var aus = new ApplianceUsageSchedule(new MockReadOnlySiteSettings());
            Assert.Empty(aus.UsageIntervals);
        }
    }
}