using System;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.UnitTests.Builders;

namespace SolarTally.UnitTests.Domain.Entities
{
    public class ConsumptionCtor
    {
        [Fact]
        void SetsSiteToCtorPassedSite()
        {
            var site = new SiteBuilder().Build();
            var consumption = new Consumption(site);
            Assert.Equal(site.Name, consumption.Site.Name);
        }

        [Fact]
        void CreatesEmptyApplianceUsages()
        {
            var site = new SiteBuilder().Build();
            var consumption = new Consumption(site);
            Assert.Equal(0, consumption.ApplianceUsages.Count);
        }
    }
}