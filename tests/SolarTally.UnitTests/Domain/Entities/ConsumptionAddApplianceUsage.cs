using System.Linq;
using Xunit;
using SolarTally.Domain.Entities;

namespace SolarTally.UnitTests.Domain.Entities
{
    public class ConsumptionAddApplianceUsage
    {
        private string _siteName = "A_SITE_NAME";
        private string _applianceName = "AN_APPLIANCE";
        private string _applianceDesc = "AN_APPLIANCE_DESC";
        private decimal _applianceDefaultPowerConsumption = 20.5m;

        Consumption CreateConsumption()
        {
            return new Consumption(new Site(_siteName));
        }
        Appliance CreateAppliance()
        {
            return new Appliance(_applianceName, _applianceDesc,
                _applianceDefaultPowerConsumption);
        }

        [Fact]
        void AddsAnApplianceUsage()
        {
            var consumption = CreateConsumption();
            consumption.AddApplianceUsage(CreateAppliance());
            Assert.Equal(1, consumption.ApplianceUsages.Count);
        }

        [Fact]
        void AddsAnApplianceUsageWithTheCorrectAppliance()
        {
            var consumption = CreateConsumption();
            var appliance = CreateAppliance();
            consumption.AddApplianceUsage(appliance);
            var foundAppliance = consumption.ApplianceUsages.First().Appliance;
            
            Assert.Equal(_applianceName, foundAppliance.Name);
            Assert.Equal(_applianceDesc, foundAppliance.Description);
            Assert.Equal(_applianceDefaultPowerConsumption,
                foundAppliance.DefaultPowerConsumption);
        }

        [Fact]
        void AddsAnApplianceUsageWithCorrectUsage()
        {
            var consumption = CreateConsumption();
            var appliance = CreateAppliance();
            consumption.AddApplianceUsage(appliance);
            var foundApplianceUsage = consumption.ApplianceUsages.First();

            Assert.Equal(1, foundApplianceUsage.Usage.Quantity);
            Assert.Equal(appliance.DefaultPowerConsumption,
                foundApplianceUsage.Usage.PowerConsumption);
            Assert.Equal(2, foundApplianceUsage.Usage.NumHours);
            Assert.Equal(1, foundApplianceUsage.Usage.PercentHrsOnSolar);
        }
    }
}