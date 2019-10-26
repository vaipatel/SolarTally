using System;
using SolarTally.Domain.Entities;
using SolarTally.Domain.Interfaces;

namespace SolarTally.UnitTests.Builders
{
    public class ApplianceUsageBuilder
    {
        private ApplianceUsage _applianceUsage;
        public IConsumptionCalculator ConsumptionCalculator;
        private Appliance _appliance;
        public Appliance TestAppliance => _appliance;
        public int TestQuantity => 2;
        public decimal TestPowerConsumption => 20.5m;
        public int TestNumHours => 3;
        public decimal TestPercentHrsOnSolar => 1;
        public bool TestEnabled => true;

        public ApplianceUsageBuilder()
        {
            _appliance = new ApplianceBuilder().Build();
            ConsumptionCalculator = new SiteBuilder().Build().Consumption;
            _applianceUsage = new ApplianceUsage(ConsumptionCalculator,
                TestAppliance, TestQuantity, TestPowerConsumption, TestNumHours,
                TestPercentHrsOnSolar, TestEnabled);
        }

        public ApplianceUsage Build()
        {
            return _applianceUsage;
        }
    }
}