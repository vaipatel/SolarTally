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
        void ThrowsForNullName()
        {
            var builder = new ApplianceBuilder();
            Assert.Throws<ArgumentNullException>(() => {
                new Appliance(null, builder.TestDescription,
                    builder.TestDefaultPowerConsumption);
            });
        }

        [Fact]
        void ThrowsForEmptyName()
        {
            var builder = new ApplianceBuilder();
            Assert.Throws<ArgumentException>(() => {
                new Appliance("", builder.TestDescription,
                    builder.TestDefaultPowerConsumption);
            });
        }

        [Fact]
        void AllowsEmptyDescription()
        {
            var builder = new ApplianceBuilder();
            var appliance = builder.WithoutDescription();

            Assert.Equal("", appliance.Description);
        }
    }
}