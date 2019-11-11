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
                    builder.TestNumHoursOnSolar, builder.TestNumHoursOffSolar,
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
                    builder.TestNumHoursOnSolar, builder.TestNumHoursOffSolar,
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
                    -10.5m, builder.TestNumHoursOnSolar,
                    builder.TestNumHoursOffSolar, builder.TestEnabled);
            });
        }

        [Fact]
        void ThrowsForOutOfRangeNumHoursOnSolar()
        {
            var builder = new ApplianceUsageBuilder();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                new ApplianceUsage(builder.TestConsumptionCalculator,
                    builder.TestAppliance, builder.TestQuantity,
                    builder.TestPowerConsumption,
                    -1, builder.TestNumHoursOffSolar, builder.TestEnabled);
            });

            Assert.Throws<ApplianceUsageHoursInvalidException>(() => {
                new ApplianceUsage(builder.TestConsumptionCalculator,
                    builder.TestAppliance, builder.TestQuantity,
                    builder.TestPowerConsumption,
                    25, builder.TestNumHoursOffSolar, builder.TestEnabled);
            });
        }

        [Fact]
        void ThrowsForOutOfRangeNumHoursOffSolar()
        {
            var builder = new ApplianceUsageBuilder();
            var testNumHoursOnSolar = builder.TestNumHoursOnSolar;
            
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                new ApplianceUsage(builder.TestConsumptionCalculator,
                    builder.TestAppliance, builder.TestQuantity,
                    builder.TestPowerConsumption,
                    testNumHoursOnSolar, -1, builder.TestEnabled);
            });

            var excessOffSolarHours = 24 - testNumHoursOnSolar + 1;
            Assert.Throws<ApplianceUsageHoursInvalidException>(() => {
                new ApplianceUsage(builder.TestConsumptionCalculator,
                    builder.TestAppliance, builder.TestQuantity,
                    builder.TestPowerConsumption,
                    testNumHoursOnSolar, excessOffSolarHours,
                    builder.TestEnabled);
            });
        }
    }
}