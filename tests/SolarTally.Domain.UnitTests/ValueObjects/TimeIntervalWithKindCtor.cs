using System;
using Xunit;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Enumerations;

namespace SolarTally.Domain.UnitTests.ValueObjects
{
    public class TimeIntervalWithKindCtor
    {
        [Fact]
        void ShouldDefaultToUsingSolarKind()
        {
            var ti = new TimeInterval(1,0,2,0);
            var tiwk = new TimeIntervalWithKind(ti);
            Assert.Equal(TimeIntervalKind.UsingSolar, tiwk.TimeIntervalKind);
        }
    }
}