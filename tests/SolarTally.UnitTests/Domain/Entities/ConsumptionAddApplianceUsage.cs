using System.Linq;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.UnitTests.Builders;

namespace SolarTally.UnitTests.Domain.Entities
{
    public class ConsumptionAddApplianceUsage
    {
        private string _siteName = "A_SITE_NAME";
        private int _siteNumSolarHours = 8;

        Consumption CreateConsumption()
        {
            return new Consumption(new Site(_siteName, _siteNumSolarHours));
        }

        [Fact]
        void AddsAnApplianceUsage()
        {
            var consumption = CreateConsumption();
            var appliance = new ApplianceBuilder().Build();
            consumption.AddApplianceUsage(appliance);
            Assert.Equal(1, consumption.ApplianceUsages.Count);
        }

        [Fact]
        void AddsAnApplianceUsageWithTheCorrectAppliance()
        {
            var consumption = CreateConsumption();
            var appliance = new ApplianceBuilder().Build();
            consumption.AddApplianceUsage(appliance);
            var foundAppliance = consumption.ApplianceUsages.First().Appliance;
            
            Assert.Equal(appliance.Name, foundAppliance.Name);
            Assert.Equal(appliance.Description, foundAppliance.Description);
            Assert.Equal(appliance.DefaultPowerConsumption,
                foundAppliance.DefaultPowerConsumption);
        }

        [Fact]
        void AddsAnApplianceUsageWithCorrectUsage()
        {
            var consumption = CreateConsumption();
            var appliance = new ApplianceBuilder().Build();
            consumption.AddApplianceUsage(appliance);
            var foundApplianceUsage = consumption.ApplianceUsages.First();

            Assert.Equal(1, foundApplianceUsage.Quantity);
            Assert.Equal(appliance.DefaultPowerConsumption,
                foundApplianceUsage.PowerConsumption);
            Assert.Equal(2, foundApplianceUsage.NumHours);
            Assert.Equal(1, foundApplianceUsage.PercentHrsOnSolar);
        }
    }
}