using System;
using SolarTally.Domain.Entities;
using SolarTally.Domain.Interfaces;

namespace SolarTally.Domain.UnitTests.Builders
{
    public class ApplianceUsageBuilder
    {
        private ApplianceUsage _applianceUsage;
        public IConsumptionCalculator TestConsumptionCalculator;
        private Appliance _appliance;
        public Appliance TestAppliance => _appliance;
        public int TestQuantity => 2;
        public decimal TestPowerConsumption => 20.5m;
        public int TestNumHours => 3;
        public decimal TestPercentHrsOnSolar => 1;
        public int TestNumHoursOnSolar => 2;
        public bool TestEnabled => true;

        public ApplianceUsageBuilder()
        {
            _appliance = new ApplianceBuilder().Build();
            TestConsumptionCalculator = new SiteBuilder().Build().Consumption;
            _applianceUsage = new ApplianceUsage(TestConsumptionCalculator,
                TestAppliance, TestQuantity, TestPowerConsumption, TestNumHours,
                TestPercentHrsOnSolar, TestEnabled);
        }

        public ApplianceUsage Build()
        {
            return _applianceUsage;
        }
    }
}