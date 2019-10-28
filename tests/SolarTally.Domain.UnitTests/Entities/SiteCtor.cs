using System;
using Xunit;
using SolarTally.Domain.Entities;

namespace SolarTally.Domain.UnitTests.Entities
{
    public class SiteCtor
    {
        private string _siteName = "A_SITE_NAME";
        private int _siteNumSolarHours = 8;

        [Fact]
        public void ThrowsIfNegativeNumSolarHours()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { 
                var site = new Site(_siteName, -1);
            });
        }

        [Fact]
        public void SetsSiteName()
        {
            var site = new Site(_siteName, _siteNumSolarHours);
            Assert.Equal(_siteName, site.Name);
        }
        
        [Fact]
        public void CreatesAConsumption()
        {
            var site = new Site(_siteName, _siteNumSolarHours);
            Assert.NotNull(site.Consumption);
        }

        [Fact]
        public void CreatesConsumptionWithThisSite()
        {
            var site = new Site(_siteName, _siteNumSolarHours);
            Assert.Equal(site.Name, site.Consumption.Site.Name);
        }
    }
}
