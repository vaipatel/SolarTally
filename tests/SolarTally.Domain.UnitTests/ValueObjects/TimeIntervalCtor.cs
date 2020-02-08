using System.Collections.Generic;
using Xunit;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Exceptions;
using System;

namespace SolarTally.Domain.UnitTests.ValueObjects
{
    public class TimeIntervalCtor
    {
        [Theory]
        [InlineData(-1, 50, 3, 1)]
        [InlineData(24, 50, 25, 1)]
        [InlineData(1, -1, 3, 1)]
        [InlineData(1, 60, 3, 1)]
        [InlineData(1, 50, -1, 1)]
        [InlineData(1, 50, 24, 1)]
        [InlineData(1, 50, 3, -1)]
        [InlineData(1, 50, 3, 60)]
        public void ShouldThrowForOutOfBounds(int startHr, int startMin,
            int endHr, int endMin)
        {
            Assert.Throws<TimeIntervalArgumentInvalidException>(() => {
                var ti = new TimeInterval(startHr, startMin, endHr, endMin);
            });
        }

        [Theory]
        [InlineData(1,0,0,1)]
        [InlineData(23,59,23,58)]
        public void ShouldThrowForStartMoreThanEnd(int startHr, int startMin,
            int endHr, int endMin)
        {
            Assert.Throws<TimeIntervalArgumentInvalidException>(() => {
                var ti = new TimeInterval(startHr, startMin, endHr, endMin);
            });
        }

        [Theory]
        [ClassData(typeof(ValidTimeIntervalData))]
        public void ShouldMakeValidTimeInterval(int startHr, int startMin,
            int endHr, int endMin, TimeSpan expStart, TimeSpan expEnd,
            TimeSpan expDiff)
        {
            var ti = new TimeInterval(startHr, startMin, endHr, endMin);
            Assert.Equal(expStart, ti.Start);
            Assert.Equal(expEnd, ti.End);
            Assert.Equal(expDiff, ti.Difference);
        }

        public class ValidTimeIntervalData : TheoryData<int, int, int, int,
            TimeSpan, TimeSpan, TimeSpan>
        {
            public class ValidTimeIntervalItem
            {
                public int StartHr { get; set; }
                public int StartMin { get; set; }
                public int EndHr { get; set; }
                public int EndMin { get; set; }
                public TimeSpan ExpStart { get; set; }
                public TimeSpan ExpEnd { get; set; }
                public TimeSpan ExpDiff{ get; set; }
            }

            public IEnumerable<ValidTimeIntervalItem> TestData =>
                new List<ValidTimeIntervalItem>()
            {
                // 1. Hours are both 0
                new ValidTimeIntervalItem()
                {
                    StartHr    = 0, StartMin = 0,
                    EndHr      = 1, EndMin   = 0,
                    ExpStart = new TimeSpan(0, 0, 0),
                    ExpEnd   = new TimeSpan(1, 0, 0),
                    ExpDiff  = new TimeSpan(1, 0, 0)
                },
                // 2. Hours are both 23
                new ValidTimeIntervalItem()
                {
                    StartHr    = 23, StartMin = 0,
                    EndHr      = 23, EndMin   = 1,
                    ExpStart = new TimeSpan(23, 0, 0),
                    ExpEnd   = new TimeSpan(23, 1, 0),
                    ExpDiff  = new TimeSpan(0, 1, 0)
                },
                // 3. General
                new ValidTimeIntervalItem()
                {
                    StartHr    =  8, StartMin = 3,
                    EndHr      = 15, EndMin   = 1,
                    ExpStart = new TimeSpan(8, 3, 0),
                    ExpEnd   = new TimeSpan(15, 1,  0),
                    ExpDiff  = new TimeSpan(6, 58, 0)
                }
            };

            public ValidTimeIntervalData()
            {
                foreach(var ti in TestData)
                {
                    Add(
                        ti.StartHr,
                        ti.StartMin,
                        ti.EndHr,
                        ti.EndMin,
                        ti.ExpStart,
                        ti.ExpEnd,
                        ti.ExpDiff
                    );
                }
            }
        }
    }
}