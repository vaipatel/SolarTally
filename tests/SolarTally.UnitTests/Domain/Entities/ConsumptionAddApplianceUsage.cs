using System;
using System.Linq;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.UnitTests.Builders;

namespace SolarTally.UnitTests.Domain.Entities
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

            Assert.Equal(ApplianceUsage.DefaultQuantity,
                foundApplianceUsage.Quantity);
            Assert.Equal(appliance.DefaultPowerConsumption,
                foundApplianceUsage.PowerConsumption);
            Assert.Equal(consumption.Site.NumSolarHours,
                foundApplianceUsage.NumHours);
            Assert.Equal(ApplianceUsage.DefaultPercentHrsOnSolar,
                foundApplianceUsage.PercentHrsOnSolar);
        }

        // [Fact]
        // void AddsAnApplianceUsageWithCorrectId()
        // {
        //     var consumption = CreateConsumption();
        //     var appliance = new ApplianceBuilder().Build();
        //     int id = 24;
        //     consumption.AddApplianceUsageWithId(id, appliance);
        //     var foundApplianceUsage = consumption.ApplianceUsages.Last();

        //     Assert.Equal(id, foundApplianceUsage.Id);
        // }

        // [Fact]
        // void ThrowsIfAddingApplianceUsageWithRepeatId()
        // {
        //     var consumption = CreateConsumption();
        //     var appliance1 = new ApplianceBuilder().Build();
        //     var appliance2 = new ApplianceBuilder().Build();
        //     int id = 24;
        //     consumption.AddApplianceUsageWithId(id, appliance1);
        //     Assert.Throws<ArgumentException>(() =>
        //         consumption.AddApplianceUsageWithId(id, appliance2));
        // }
    }
}