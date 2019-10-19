using System;
using Xunit;
using SolarTally.Domain.Entities;

namespace SolarTally.UnitTests.Domain.Entities
{
    public class SiteCtor
    {
        private string _siteName = "A_SITE_NAME";

        [Fact]
        public void ThrowsIfSiteNameEmpty()
        {
            Assert.Throws<ArgumentException>(() => { 
                var site = new Site("");
            });
        }

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
    }
}
