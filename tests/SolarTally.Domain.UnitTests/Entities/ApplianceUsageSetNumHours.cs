using System;
using Xunit;
using SolarTally.Domain.Exceptions;
using SolarTally.Domain.UnitTests.Builders;

namespace SolarTally.Domain.UnitTests.Entities
{
    public class ApplianceUsageSetNumHours
    {
        [Fact]
        void ThrowsIfNumHoursOnSolarNegative()
        {
            var applianceUsage = new ApplianceUsageBuilder().Build();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                applianceUsage.SetNumHoursOnSolar(-1);
            });
        }

        [Fact]
        void ThrowsForMoreThanSiteNumSolarHours()
        {
            var builder = new ApplianceUsageBuilder();
            var applianceUsage = builder.Build();
            var siteNumSolarHours =
                builder.TestConsumptionCalculator.GetSiteNumSolarHours();
            Assert.Throws<ApplianceUsageHoursInvalidException>(() => {
                applianceUsage.SetNumHoursOnSolar(siteNumSolarHours + 1);
            });
            
        }

        [Fact]
        void ThrowsIfNumHoursOnSolarTooMuchEvenIfLessThanNumSolarHours()
        {
            var applianceUsage = new ApplianceUsageBuilder().Build();
            applianceUsage.SetNumHoursOnSolar(0);
            applianceUsage.SetNumHoursOffSolar(24);
            Assert.Throws<ApplianceUsageHoursInvalidException>(() => {
                applianceUsage.SetNumHoursOnSolar(1);
            });
        }

        [Fact]
        void ThrowsIfNumHoursOffSolarNegative()
        {
            var applianceUsage = new ApplianceUsageBuilder().Build();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                applianceUsage.SetNumHoursOffSolar(-1);
            });
        }

        [Fact]
        void ThrowsIfNumHoursOffSolarTooMuch()
        {
            var applianceUsage = new ApplianceUsageBuilder().Build();
            var currHoursOnSolar = applianceUsage.NumHoursOnSolar;
            var excessHoursOffSolar = 24 - currHoursOnSolar + 1;
            Assert.Throws<ApplianceUsageHoursInvalidException>(() => {
                applianceUsage.SetNumHoursOffSolar(excessHoursOffSolar);
            });
        }
    }
}