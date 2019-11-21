using System;
using SolarTally.Domain.Entities;
using SolarTally.Domain.Enumerations;
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
        public int TestNumHoursOnSolar => 2;
        public int TestNumHoursOffSolar => 1;
        public bool TestEnabled => true;

        public ApplianceUsageBuilder()
        {
            _appliance = new ApplianceBuilder().Build();
            TestConsumptionCalculator = new SiteBuilder().Build().Consumption;
            _applianceUsage = new ApplianceUsage(TestConsumptionCalculator,
                TestAppliance, TestQuantity, TestPowerConsumption, TestEnabled);
            _applianceUsage.ApplianceUsageSchedule.ClearUsageIntervals();
            _applianceUsage.ApplianceUsageSchedule.AddUsageInterval(8,0,10,0,
                UsageKind.UsingSolar);
            _applianceUsage.ApplianceUsageSchedule.AddUsageInterval(18,0,19,0,
                UsageKind.UsingMains);
        }

        public ApplianceUsage Build()
        {
            return _applianceUsage;
        }
    }
}