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
            var ti = new TimeInterval(new DateTime(1,1,1,1,0,0),
                new DateTime(1,1,1,2,0,0));
            var tiwk = new TimeIntervalWithKind(ti);
            Assert.Equal(TimeIntervalKind.UsingSolar, tiwk.TimeIntervalKind);
        }
    }
}