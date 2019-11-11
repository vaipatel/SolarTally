using System.Collections.Generic;
using System.Linq;
using Xunit;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Entities;
using SolarTally.Domain.UnitTests.Builders;

namespace SolarTally.Domain.UnitTests.ValueObjects
{
    public class ConsumptionTotalCtor
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void ShouldCalcTheCorrectTotals(
            List<CompactApplianceUsage> applianceUsages,
            decimal expTotalPowerConsumption,
            decimal expTotalOnSolarEnergyConsumption,
            decimal expTotalOffSolarEnergyConsumption,
            decimal expTotalEnergyConsumption)
        {
            var site = new SiteBuilder().Build();
            for (int i = 0; i < applianceUsages.Count; ++i)
            {
                // Add a new Applianceusage
                var appliance = new ApplianceBuilder().Build();
                site.Consumption.AddApplianceUsage(appliance);
                // Modify it based on the passed test data
                var applianceUsage = site.Consumption.ApplianceUsages.Last();
                applianceUsage.SetQuantity(applianceUsages[i].Quantity);
                applianceUsage.SetPowerConsumption(
                    applianceUsages[i].PowerConsumption);
                applianceUsage.SetNumHoursOnSolar(
                    applianceUsages[i].NumHoursOnSolar);
                applianceUsage.SetNumHoursOffSolar(
                    applianceUsages[i].NumHoursOffSolar);
            }

            Assert.Equal(expTotalPowerConsumption,
                site.Consumption.ConsumptionTotal.TotalPowerConsumption);
            Assert.Equal(expTotalOnSolarEnergyConsumption,
                site.Consumption.ConsumptionTotal
                .TotalOnSolarEnergyConsumption);
            Assert.Equal(expTotalOffSolarEnergyConsumption,
                site.Consumption.ConsumptionTotal
                .TotalOffSolarEnergyConsumption);
            Assert.Equal(expTotalEnergyConsumption,
                site.Consumption.ConsumptionTotal.TotalEnergyConsumption);
        }

        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                // All zeros
                new object[] {
                    new List<CompactApplianceUsage>{
                        new CompactApplianceUsage(0, 0, 0, 0),
                        new CompactApplianceUsage(2, 0, 0, 0),
                        new CompactApplianceUsage(0, 20.6m, 0, 0),
                        new CompactApplianceUsage(0, 0, 3, 3),
                        new CompactApplianceUsage(0, 20.6m, 3, 3),
                        new CompactApplianceUsage(2, 0, 3, 3)
                    },
                    0, 0, 0, 0
                },
                // Clones
                new object[] {
                    new List<CompactApplianceUsage>{
                        new CompactApplianceUsage(2, 20, 3, 2),
                        new CompactApplianceUsage(2, 20, 3, 2),
                        new CompactApplianceUsage(2, 20, 3, 2),
                    },
                    120, 360, 240, 600
                },
                // Different ones
                new object[] {
                    new List<CompactApplianceUsage>{
                        new CompactApplianceUsage(1, 20, 3, 1),
                        new CompactApplianceUsage(2, 20, 3, 2),
                        new CompactApplianceUsage(3, 20.6m, 4, 5)
                    },
                    121.8, 427.2, 409, 836.2
                }
            };
        
        public class CompactApplianceUsage
        {
            public int Quantity { get; set; }
            public decimal PowerConsumption { get; set; }
            public int NumHoursOnSolar { get; set; }
            public int NumHoursOffSolar { get; set; }

            public CompactApplianceUsage(int quantity, decimal powerConsumption,
                int numHoursOnSolar, int numHoursOffSolar)
            {
                Quantity = quantity;
                PowerConsumption = powerConsumption;
                NumHoursOnSolar = numHoursOnSolar;
                NumHoursOffSolar = numHoursOffSolar;
            }
        }
    }
}