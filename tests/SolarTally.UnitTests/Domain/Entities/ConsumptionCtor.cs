using System;
using Xunit;
using SolarTally.Domain.Entities;

namespace SolarTally.UnitTests.Domain.Entities
{
    public class ConsumptionCtor
    {
        private string _siteName = "A_SITE_NAME";
        [Fact]
        void SetsSiteToCtorPassedSite()
        {
            var consumption = new Consumption(new Site(_siteName));
            Assert.Equal(_siteName, consumption.Site.Name);
        }

        [Fact]
        void CreatesEmptyApplianceUsages()
        {
            var consumption = new Consumption(new Site(_siteName));
            Assert.Equal(0, consumption.ApplianceUsages.Count);
        }
    }
}