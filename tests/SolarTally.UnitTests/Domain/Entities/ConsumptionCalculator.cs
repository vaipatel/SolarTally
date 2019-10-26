using System;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.UnitTests.Builders;
using SolarTally.Domain.Interfaces;

namespace SolarTally.UnitTests.Domain.Entities
{
    public class ConsumptionCalculator
    {
        [Fact]
        void GetsSiteNumSolarHours()
        {
            var site = new SiteBuilder().Build();
            var consumption = new Consumption(site);
            var expectedHrs = site.NumSolarHours;
            Assert.Equal(expectedHrs, consumption.GetSiteNumSolarHours());
        }

        [Fact]
        void GetsUpdatedSiteNumSolarHours()
        {
            var site = new SiteBuilder().Build();
            var consumption = new Consumption(site);
            var hrs = site.NumSolarHours + 1; // add an hour
            site.SetNumSolarHours(hrs);
            Assert.Equal(hrs, consumption.GetSiteNumSolarHours());
        }
    }
}