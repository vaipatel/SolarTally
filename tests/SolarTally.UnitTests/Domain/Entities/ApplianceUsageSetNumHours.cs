using System;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.UnitTests.Builders;

namespace SolarTally.UnitTests.Domain.Entities
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