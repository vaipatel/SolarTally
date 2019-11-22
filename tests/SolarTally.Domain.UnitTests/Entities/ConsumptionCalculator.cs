using System;
using Xunit;
using SolarTally.Domain.Entities;
using SolarTally.Domain.Interfaces;
using SolarTally.Domain.UnitTests.Builders;
using SolarTally.Domain.ValueObjects;

namespace SolarTally.Domain.UnitTests.Entities
{
    public class ConsumptionCalculator
    {
        [Fact]
        void GetsSiteNumSolarHours()
        {
            var site = new SiteBuilder().Build();
            var consumption = new Consumption(site);
            var expectedHrs = site.NumSolarHours;
            Assert.Equal(expectedHrs, 
                consumption.ReadOnlySiteSettings.NumSolarHours);
        }

        [Fact]
        void GetsUpdatedSiteNumSolarHours()
        {
            var site = new SiteBuilder().Build();
            var consumption = new Consumption(site);
            var hrs = site.NumSolarHours + 1; // add an hour
            var ti = site.PeakSolarInterval;
            int startHr = ti.Start.Hours, startMin = ti.Start.Minutes;
            int endHr   = ti.End.Hours,   endMin   = ti.End.Minutes;
            site.SetPeakSolarInterval(new TimeInterval(startHr, startMin,
                endHr + 1, endMin));
            Assert.Equal(hrs, 
                consumption.ReadOnlySiteSettings.NumSolarHours);
        }
    }
}