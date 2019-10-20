using System;
using SolarTally.Domain.Entities;

namespace SolarTally.UnitTests.Builders
{
    public class ApplianceBuilder
    {
        private Appliance _appliance;
        public string TestName => "LED Bulb";
        public string TestDescription => "A very efficient source of light";
        public decimal TestDefaultPowerConsumption => 20.5m;

        public ApplianceBuilder()
        {
            _appliance = new Appliance(TestName, TestDescription,
                TestDefaultPowerConsumption);
        }

        public Appliance Build()
        {
            return _appliance;
        }

        public Appliance WithoutNameAndDescription()
        {
            _appliance = new Appliance("", "", TestDefaultPowerConsumption);
            return _appliance;
        }
    }
}