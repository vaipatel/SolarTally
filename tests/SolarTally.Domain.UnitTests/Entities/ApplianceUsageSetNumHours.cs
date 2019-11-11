using System;
using Xunit;
using SolarTally.Domain.Exceptions;
using SolarTally.Domain.UnitTests.Builders;

namespace SolarTally.Domain.UnitTests.Entities
{
    public class ApplianceUsageSetNumHours
    {
        [Fact]
        void ThrowsForNegativeNumHoursOnSolar()
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
            var currNumSolarHours = applianceUsage.NumHoursOnSolar;
            applianceUsage.SetNumHoursOnSolar(0);
            applianceUsage.SetNumHoursOffSolar(24);
            Assert.Throws<ApplianceUsageHoursInvalidException>(() => {
                applianceUsage.SetNumHoursOnSolar(1);
            });
        }
    }
}