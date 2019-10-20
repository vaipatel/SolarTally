using System;
using Xunit;
using SolarTally.Domain.Entities;

namespace SolarTally.UnitTests.Domain.Entities
{
    public class ApplianceCtor
    {
        private string _applianceName = "AN_APPLIANCE";
        private string _applianceDesc = "AN_APPLIANCE_DESC";

        [Fact]
        void ThrowsForNegativeDefaultPowerConsumption()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                new Appliance(_applianceName, _applianceDesc, -10.5m);
            });
        }

        [Fact]
        void AllowsEmptyNameAndDescription()
        {
            var appliance = new Appliance("", "", 10.5m);

            Assert.Equal("", appliance.Name);
            Assert.Equal("", appliance.Description);
        }
    }
}