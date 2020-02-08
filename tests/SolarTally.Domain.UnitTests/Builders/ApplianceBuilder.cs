using System;
using SolarTally.Domain.Entities;

namespace SolarTally.Domain.UnitTests.Builders
{
    public class ApplianceBuilder
    {
        private Appliance _appliance;
        public string TestName => "LED Bulb";
        public string TestDescription => "A very efficient source of light";
        public decimal TestDefaultPowerConsumption => 20.5m;
        public decimal TestDefaultStartupPowerConsumption => 30;

        public ApplianceBuilder()
        {
            _appliance = new Appliance(TestName, TestDescription,
                TestDefaultPowerConsumption,
                TestDefaultStartupPowerConsumption);
        }

        public Appliance Build()
        {
            return _appliance;
        }

        public Appliance WithoutDescription()
        {
            _appliance = new Appliance(TestName, "",
                TestDefaultPowerConsumption,
                TestDefaultStartupPowerConsumption);
            return _appliance;
        }
    }
}