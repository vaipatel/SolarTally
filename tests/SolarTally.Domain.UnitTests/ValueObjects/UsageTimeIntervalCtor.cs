using System;
using Xunit;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Enumerations;

namespace SolarTally.Domain.UnitTests.ValueObjects
{
    public class UsageTimeIntervalCtor
    {
        [Fact]
        void ShouldDefaultToUsingSolarKind()
        {
            var ti = new TimeInterval(1,0,2,0);
            var tiwk = new UsageTimeInterval(ti);
            Assert.Equal(UsageKind.UsingSolar, tiwk.UsageKind);
        }
    }
}