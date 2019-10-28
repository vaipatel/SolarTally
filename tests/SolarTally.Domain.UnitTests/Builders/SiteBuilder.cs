using System;
using SolarTally.Domain.Entities;

namespace SolarTally.Domain.UnitTests.Builders
{
    public class SiteBuilder
    {
        private Site _site;
        public string TestName => "Mac's convenience";
        public int TestNumSolarHours => 8;

        public SiteBuilder()
        {
            _site = new Site(TestName, TestNumSolarHours);
        }

        public Site Build()
        {
            return _site;
        }

        public Site WithoutName()
        {
            _site = new Site("", TestNumSolarHours);
            return _site;
        }
    }
}