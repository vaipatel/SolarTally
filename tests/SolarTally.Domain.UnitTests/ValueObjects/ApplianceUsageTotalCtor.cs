using System.Collections.Generic;
using Xunit;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Entities;
using SolarTally.Domain.UnitTests.Builders;

namespace SolarTally.Domain.UnitTests.ValueObjects
{
    public class ApplianceUsageTotalCtor
    {
        [Theory]
        [ClassData(typeof(AUTestData))]
        public void ShouldCalcTheCorrectTotals(
            int quantity,
            decimal powerConsumption,
            int numHoursOnSolar,
            int numHoursOffSolar,
            decimal expTotalPowerConsumption,
            decimal expTotalOnSolarEnergyConsumption,
            decimal expTotalOffSolarEnergyConsumption,
            decimal expTotalEnergyConsumption)
        {
            var builder = new ApplianceUsageBuilder();
            var applianceUsage = new ApplianceUsage(
                builder.TestConsumptionCalculator, builder.TestAppliance,
                quantity, powerConsumption, builder.TestNumHours,
                numHoursOnSolar, numHoursOffSolar, builder.TestEnabled);
            var applianceUsageTotal = new ApplianceUsageTotal(applianceUsage);

            Assert.Equal(expTotalPowerConsumption,
                applianceUsageTotal.TotalPowerConsumption);
            Assert.Equal(expTotalOnSolarEnergyConsumption,
                applianceUsageTotal.TotalOnSolarEnergyConsumption);
            Assert.Equal(expTotalOffSolarEnergyConsumption,
                applianceUsageTotal.TotalOffSolarEnergyConsumption);
            Assert.Equal(expTotalEnergyConsumption,
                applianceUsageTotal.TotalEnergyConsumption);
        }

        public class AUTestDataItem
        {
            public int Quantity { get; set; }
            public decimal PowerConsumption { get; set; }
            public int NumHoursOnSolar { get; set; }
            public int NumHoursOffSolar { get; set; }
            public decimal ExpTotalPowerConsumption { get; set; }
            public decimal ExpTotalOnSolarEnergyConsumption { get; set; }
            public decimal ExpTotalOffSolarEnergyConsumption { get; set; }
            public decimal ExpTotalEnergyConsumption { get; set; }
        }

        public static IEnumerable<AUTestDataItem> TestData =>
            new List<AUTestDataItem>
            {
                // All zeros
                new AUTestDataItem {
                    Quantity = 0,
                    PowerConsumption = 0,
                    NumHoursOnSolar = 0,
                    NumHoursOffSolar = 0,
                    ExpTotalPowerConsumption = 0,
                    ExpTotalOnSolarEnergyConsumption = 0,
                    ExpTotalOffSolarEnergyConsumption = 0,
                    ExpTotalEnergyConsumption = 0
                },
                // Zero Quantity
                new AUTestDataItem {
                    Quantity = 0,
                    PowerConsumption = 20,
                    NumHoursOnSolar = 7,
                    NumHoursOffSolar = 3,
                    ExpTotalPowerConsumption = 0,
                    ExpTotalOnSolarEnergyConsumption = 0,
                    ExpTotalOffSolarEnergyConsumption = 0,
                    ExpTotalEnergyConsumption = 0
                },
                // Zero PowerConsumption
                new AUTestDataItem {
                    Quantity = 1,
                    PowerConsumption = 0,
                    NumHoursOnSolar = 7,
                    NumHoursOffSolar = 3,
                    ExpTotalPowerConsumption = 0,
                    ExpTotalOnSolarEnergyConsumption = 0,
                    ExpTotalOffSolarEnergyConsumption = 0,
                    ExpTotalEnergyConsumption = 0
                },
                // Zero NumHoursOnSolar
                new AUTestDataItem {
                    Quantity = 1,
                    PowerConsumption = 20,
                    NumHoursOnSolar = 0,
                    NumHoursOffSolar = 3,
                    ExpTotalPowerConsumption = 20,
                    ExpTotalOnSolarEnergyConsumption = 0,
                    ExpTotalOffSolarEnergyConsumption = 60,
                    ExpTotalEnergyConsumption = 60
                },
                // Zero NumHoursOffSolar
                new AUTestDataItem {
                    Quantity = 1,
                    PowerConsumption = 20,
                    NumHoursOnSolar = 3,
                    NumHoursOffSolar = 0,
                    ExpTotalPowerConsumption = 20,
                    ExpTotalOnSolarEnergyConsumption = 60,
                    ExpTotalOffSolarEnergyConsumption = 0,
                    ExpTotalEnergyConsumption = 60
                },
                // Quantity=NumOnSolarHours=NumOffSolarHours=1
                new AUTestDataItem {
                    Quantity = 1,
                    PowerConsumption = 20,
                    NumHoursOnSolar = 1,
                    NumHoursOffSolar = 1,
                    ExpTotalPowerConsumption = 20,
                    ExpTotalOnSolarEnergyConsumption = 20,
                    ExpTotalOffSolarEnergyConsumption = 20,
                    ExpTotalEnergyConsumption = 40
                },
                // Quantity=NumOnSolarHours=NumOffSolarHours=2
                new AUTestDataItem {
                    Quantity = 2,
                    PowerConsumption = 20,
                    NumHoursOnSolar = 2,
                    NumHoursOffSolar = 2,
                    ExpTotalPowerConsumption = 40,
                    ExpTotalOnSolarEnergyConsumption = 80,
                    ExpTotalOffSolarEnergyConsumption = 80,
                    ExpTotalEnergyConsumption = 160
                },
                // Mutually unequal Quantity,NumOnSolarHours,NumOffSolarHours
                new AUTestDataItem {
                    Quantity = 2,
                    PowerConsumption = 20,
                    NumHoursOnSolar = 4,
                    NumHoursOffSolar = 3,
                    ExpTotalPowerConsumption = 2 * 20,
                    ExpTotalOnSolarEnergyConsumption = 2 * 20 * 4,
                    ExpTotalOffSolarEnergyConsumption = 2 * 20 * 3,
                    ExpTotalEnergyConsumption = 2 * 20 * (4 + 3)
                }
            };
        
        public class AUTestData : TheoryData<int, decimal, int, int, 
            decimal, decimal, decimal, decimal>
        {
            public AUTestData()
            {
                foreach(var au in TestData)
                {
                    Add(
                        au.Quantity,
                        au.PowerConsumption,
                        au.NumHoursOnSolar,
                        au.NumHoursOffSolar,
                        au.ExpTotalPowerConsumption,
                        au.ExpTotalOnSolarEnergyConsumption,
                        au.ExpTotalOffSolarEnergyConsumption,
                        au.ExpTotalEnergyConsumption
                    );
                }
            }
        }

    }
}