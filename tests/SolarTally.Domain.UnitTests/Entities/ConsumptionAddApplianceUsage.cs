using System;
using System.Linq;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.Domain.UnitTests.Builders;

namespace SolarTally.Domain.UnitTests.Entities
{
    public class ConsumptionAddApplianceUsage
    {
        Consumption CreateConsumption()
        {
            return new Consumption(new SiteBuilder().Build());
        }

        [Fact]
        void AddsAnApplianceUsage()
        {
            var consumption = CreateConsumption();
            var appliance = new ApplianceBuilder().Build();
            consumption.AddApplianceUsage(appliance);
            Assert.Single(consumption.ApplianceUsages);
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

            Assert.Equal(ApplianceUsage.DefaultQuantity,
                foundApplianceUsage.Quantity);
            Assert.Equal(appliance.DefaultPowerConsumption,
                foundApplianceUsage.PowerConsumption);
            // Default num hours on solar should be the site num solar hours
            Assert.Equal(consumption.ReadOnlySiteSettings.NumSolarHours,
                foundApplianceUsage.GetNumHoursOnSolar());
            // Default num hours off solar should be one designated in AU
            Assert.Equal(0,
                foundApplianceUsage.GetNumHoursOffSolar());
            // Total num hours = num hours on solar + num hours off solar
            Assert.Equal(
                foundApplianceUsage.GetNumHoursOnSolar() +
                foundApplianceUsage.GetNumHoursOffSolar(),
                foundApplianceUsage.GetNumHours());
        }
    }
}