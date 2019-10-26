using System;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.UnitTests.Builders;

namespace SolarTally.UnitTests.Domain.Entities
{
    public class ApplianceUsageCtor
    {
        [Fact]
        void ThrowsForNullAppliance()
        {
            var builder = new ApplianceUsageBuilder();
            Assert.Throws<ArgumentNullException>(() => {
                new ApplianceUsage(builder.ConsumptionCalculator, null,
                    builder.TestQuantity, builder.TestPowerConsumption,
                    builder.TestNumHours, builder.TestPercentHrsOnSolar,
                    builder.TestEnabled);
            });
        }

        [Fact]
        void ThrowsForNegativeQuantity()
        {
            var builder = new ApplianceUsageBuilder();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                new ApplianceUsage(builder.ConsumptionCalculator,
                    builder.TestAppliance, -1, builder.TestPowerConsumption, 
                    builder.TestNumHours, builder.TestPercentHrsOnSolar, 
                    builder.TestEnabled);
            });
        }

        [Fact]
        void ThrowsForNegativePowerConsumption()
        {
            var builder = new ApplianceUsageBuilder();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                new ApplianceUsage(builder.ConsumptionCalculator,
                    builder.TestAppliance, builder.TestQuantity,
                    -10.5m, builder.TestNumHours, builder.TestPercentHrsOnSolar,
                    builder.TestEnabled);
            });
        }

        [Fact]
        void ThrowsForNegativeNumHours()
        {
            var builder = new ApplianceUsageBuilder();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                new ApplianceUsage(builder.ConsumptionCalculator,
                    builder.TestAppliance, builder.TestQuantity,
                    builder.TestPowerConsumption, -1,
                    builder.TestPercentHrsOnSolar, builder.TestEnabled);
            });
        }

        [Fact]
        void ThrowsForGreaterThan24NumHours()
        {
            var builder = new ApplianceUsageBuilder();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                new ApplianceUsage(builder.ConsumptionCalculator,
                    builder.TestAppliance, builder.TestQuantity,
                    builder.TestPowerConsumption, 25,
                    builder.TestPercentHrsOnSolar, builder.TestEnabled);
            });
        }

        [Fact]
        void ThrowsForNegativePercentHrsOnSolar()
        {
            var builder = new ApplianceUsageBuilder();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                new ApplianceUsage(builder.ConsumptionCalculator,
                    builder.TestAppliance, builder.TestQuantity,
                    builder.TestPowerConsumption, builder.TestNumHours,
                    -1.1m, builder.TestEnabled);
            });
        }

        [Fact]
        void ThrowsForGreaterThan1PercentHrsOnSolar()
        {
            var builder = new ApplianceUsageBuilder();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                new ApplianceUsage(builder.ConsumptionCalculator,
                    builder.TestAppliance, builder.TestQuantity,
                    builder.TestPowerConsumption, builder.TestNumHours,
                    1.1m, builder.TestEnabled);
            });
        }
    }
}