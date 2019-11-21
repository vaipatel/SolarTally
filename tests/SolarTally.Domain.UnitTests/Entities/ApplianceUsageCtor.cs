using System;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.Domain.Exceptions;
using SolarTally.Domain.UnitTests.Builders;

namespace SolarTally.Domain.UnitTests.Entities
{
    public class ApplianceUsageCtor
    {
        [Fact]
        void ThrowsForNullAppliance()
        {
            var builder = new ApplianceUsageBuilder();
            Assert.Throws<ArgumentNullException>(() => {
                new ApplianceUsage(builder.TestConsumptionCalculator, null,
                    builder.TestQuantity, builder.TestPowerConsumption,
                    builder.TestEnabled);
            });
        }

        [Fact]
        void ThrowsForNegativeQuantity()
        {
            var builder = new ApplianceUsageBuilder();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                new ApplianceUsage(builder.TestConsumptionCalculator,
                    builder.TestAppliance, -1, builder.TestPowerConsumption,
                    builder.TestEnabled);
            });
        }

        [Fact]
        void ThrowsForNegativePowerConsumption()
        {
            var builder = new ApplianceUsageBuilder();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                new ApplianceUsage(builder.TestConsumptionCalculator,
                    builder.TestAppliance, builder.TestQuantity,
                    -10.5m, builder.TestEnabled);
            });
        }
    }
}