using System.Collections.Generic;
using Xunit;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Entities;
using SolarTally.UnitTests.Builders;

namespace SolarTally.UnitTests.Domain.ValueObjects
{
    public class ApplianceUsageTotalCtor
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void ShouldCalcTheCorrectTotals(int quantity,
            decimal powerConsumption, int numHours,
            decimal expTotalPowerConsumption, decimal expTotalEnergyConsumption)
        {
            var builder = new ApplianceUsageBuilder();
            var applianceUsage = new ApplianceUsage(
                builder.TestConsumptionCalculator, builder.TestAppliance,
                quantity, powerConsumption, numHours,
                builder.TestPercentHrsOnSolar, builder.TestEnabled);
            var applianceUsageTotal = new ApplianceUsageTotal(applianceUsage);

            Assert.Equal(expTotalPowerConsumption,
                applianceUsageTotal.TotalPowerConsumption);
            Assert.Equal(expTotalEnergyConsumption,
                applianceUsageTotal.TotalEnergyConsumption);
        }

        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                // All zeros
                new object[] {0, 0, 0, 0, 0},
                // Zero Quantity
                new object[] {0, 20, 1, 0, 0},
                // Zero PowerConsumption
                new object[] {1, 0, 1, 0, 0},
                // Zero NumHours
                new object[] {1, 20, 0, 20, 0},
                // Quantity=NumHours=1
                new object[] {1, 20, 1, 20, 20},
                // Quantity=NumHours=2
                new object[] {2, 20, 2, 40, 80},
                // Quantity!=NumHours
                new object[] {2, 20, 3, 40, 120}
            };

    }
}