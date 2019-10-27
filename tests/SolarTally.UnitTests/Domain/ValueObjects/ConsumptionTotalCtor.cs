using System.Collections.Generic;
using System.Linq;
using Xunit;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Entities;
using SolarTally.UnitTests.Builders;

namespace SolarTally.UnitTests.Domain.ValueObjects
{
    public class ConsumptionTotalCtor
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void ShouldCalcTheCorrectTotals(
            List<CompactApplianceUsage> applianceUsages,
            decimal expTotalPowerConsumption,
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
                applianceUsage.SetNumHours(applianceUsages[i].NumHours);
            }

            Assert.Equal(expTotalPowerConsumption,
                site.Consumption.ConsumptionTotal.TotalPowerConsumption);
            Assert.Equal(expTotalEnergyConsumption,
                site.Consumption.ConsumptionTotal.TotalEnergyConsumption);
        }

        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                // All zeros
                new object[] {
                    new List<CompactApplianceUsage>{
                        new CompactApplianceUsage(0, 0, 0),
                        new CompactApplianceUsage(2, 0, 0),
                        new CompactApplianceUsage(0, 20.6m, 0),
                        new CompactApplianceUsage(0, 0, 3),
                        new CompactApplianceUsage(0, 20.6m, 3),
                        new CompactApplianceUsage(2, 0, 3)
                    },
                    0, 0
                },
                // Clones
                new object[] {
                    new List<CompactApplianceUsage>{
                        new CompactApplianceUsage(2, 20, 3),
                        new CompactApplianceUsage(2, 20, 3),
                        new CompactApplianceUsage(2, 20, 3),
                    },
                    120, 360
                },
                // Different ones
                new object[] {
                    new List<CompactApplianceUsage>{
                        new CompactApplianceUsage(0, 20, 3),
                        new CompactApplianceUsage(2, 20, 3),
                        new CompactApplianceUsage(3, 20.6m, 4),
                    },
                    101.8, 367.2
                }
            };
        
        public class CompactApplianceUsage
        {
            public int Quantity { get; set; }
            public decimal PowerConsumption { get; set; }
            public int NumHours { get; set; }

            public CompactApplianceUsage(int quantity, decimal powerConsumption,
                int numHours)
            {
                Quantity = quantity;
                PowerConsumption = powerConsumption;
                NumHours = numHours;
            }
        }
    }
}