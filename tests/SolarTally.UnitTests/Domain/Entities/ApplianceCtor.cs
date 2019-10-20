using System;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.UnitTests.Builders;

namespace SolarTally.UnitTests.Domain.Entities
{
    public class ApplianceCtor
    {
        [Fact]
        void ThrowsForNegativeDefaultPowerConsumption()
        {
            var builder = new ApplianceBuilder();
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                new Appliance(builder.TestName, builder.TestDescription,
                -10.5m);
            });
        }

        [Fact]
        void AllowsEmptyNameAndDescription()
        {
            var builder = new ApplianceBuilder();
            var appliance = builder.WithoutNameAndDescription();

            Assert.Equal("", appliance.Name);
            Assert.Equal("", appliance.Description);
        }
    }
}