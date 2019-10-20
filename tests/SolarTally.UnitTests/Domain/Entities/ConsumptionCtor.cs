using System;
using Xunit;
using SolarTally.Domain.Entities;

namespace SolarTally.UnitTests.Domain.Entities
{
    public class ConsumptionCtor
    {
        private string _siteName = "A_SITE_NAME";
        private int _siteNumSolarHours = 8;

        [Fact]
        void SetsSiteToCtorPassedSite()
        {
            var consumption = new Consumption(new Site(_siteName,
                _siteNumSolarHours));
            Assert.Equal(_siteName, consumption.Site.Name);
        }

        [Fact]
        void CreatesEmptyApplianceUsages()
        {
            var consumption = new Consumption(new Site(_siteName,
                _siteNumSolarHours));
            Assert.Equal(0, consumption.ApplianceUsages.Count);
        }
    }
}