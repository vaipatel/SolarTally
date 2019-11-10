using System;
using Xunit;
using SolarTally.Domain.Entities;
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
                    builder.TestNumHours, builder.TestNumHoursOnSolar,
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
                    builder.TestNumHours, builder.TestNumHoursOnSolar,
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
                    -10.5m, builder.TestNumHours, builder.TestNumHoursOnSolar,
                    builder.TestEnabled);
            });
        }

        [Fact]
        void ThrowsForNegativeNumHours()
        {
            var builder = new ApplianceUsageBuilder();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                new ApplianceUsage(builder.TestConsumptionCalculator,
                    builder.TestAppliance, builder.TestQuantity,
                    builder.TestPowerConsumption, -1,
                    builder.TestNumHoursOnSolar, builder.TestEnabled);
            });
        }

        [Fact]
        void ThrowsForGreaterThan24NumHours()
        {
            var builder = new ApplianceUsageBuilder();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                new ApplianceUsage(builder.TestConsumptionCalculator,
                    builder.TestAppliance, builder.TestQuantity,
                    builder.TestPowerConsumption, 25,
                    builder.TestNumHoursOnSolar, builder.TestEnabled);
            });
        }

        [Fact]
        void ThrowsForNumHoursOnSolarGreaterThanNumHours()
        {
            var builder = new ApplianceUsageBuilder();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                new ApplianceUsage(builder.TestConsumptionCalculator,
                    builder.TestAppliance, builder.TestQuantity,
                    builder.TestPowerConsumption, builder.TestNumHours,
                    builder.TestNumHours + 1, builder.TestEnabled);
            });
        }
    }
}