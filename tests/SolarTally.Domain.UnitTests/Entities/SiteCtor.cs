using System;
using Xunit;
using SolarTally.Domain.Entities;

namespace SolarTally.Domain.UnitTests.Entities
{
    public class SiteCtor
    {
        private string _siteName = "A_SITE_NAME";
        private int _siteExpNumSolarHours = 8;

        [Fact]
        public void SetsSiteName()
        {
            var site = new Site(_siteName);
            Assert.Equal(_siteName, site.Name);
        }
        
        [Fact]
        public void CreatesAConsumption()
        {
            var site = new Site(_siteName);
            Assert.NotNull(site.Consumption);
        }

        [Fact]
        public void CreatesConsumptionWithThisSite()
        {
            var site = new Site(_siteName);
            Assert.Equal(site.Name, site.Consumption.Site.Name);
        }

        [Fact]
        public void SetsDefaultSolarInterval()
        {
            var site = new Site(_siteName);
            Assert.Equal(new TimeSpan(_siteExpNumSolarHours, 0, 0),
                site.PeakSolarInterval.Difference);
        }
    }
}
