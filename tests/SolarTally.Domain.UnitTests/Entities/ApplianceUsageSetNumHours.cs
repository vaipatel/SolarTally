using System;
using Xunit;
using SolarTally.Domain.Exceptions;
using SolarTally.Domain.UnitTests.Builders;

namespace SolarTally.Domain.UnitTests.Entities
{
    public class ApplianceUsageSetNumHours
    {
        [Fact]
        void ThrowsForNegativeNumHours()
        {
            var applianceUsage = new ApplianceUsageBuilder().Build();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                applianceUsage.SetNumHours(-1);
            });
        }

        [Fact]
        void ThrowsForGreaterThan24NumHours()
        {
            var applianceUsage = new ApplianceUsageBuilder().Build();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                applianceUsage.SetNumHours(25);
            });
        }

        [Fact]
        void ThrowsCustomExForMoreThanSiteNumSolarHours()
        {
            var builder = new ApplianceUsageBuilder();
            var applianceUsage = builder.Build();
            var siteNumSolarHours =
                builder.TestConsumptionCalculator.GetSiteNumSolarHours();
            // First make sure that PercentHrsOnSolar is 100%
            Assert.Equal(1, applianceUsage.PercentHrsOnSolar);
            // Now should throw when trying to set more than site solar hrs
            Assert.Throws<ApplianceUsageHoursInvalidException>(() => {
                applianceUsage.SetNumHours(siteNumSolarHours + 1);
            });
        }

        [Fact]
        void SetsTheNumHours()
        {
            var applianceUsage = new ApplianceUsageBuilder().Build();
            var prevNumHours = applianceUsage.NumHours;
            var currNumHours = (prevNumHours < 24) ? prevNumHours + 1 :
                prevNumHours - 1;
            applianceUsage.SetNumHours(currNumHours);
            Assert.Equal(currNumHours, applianceUsage.NumHours);
        }
    }
}