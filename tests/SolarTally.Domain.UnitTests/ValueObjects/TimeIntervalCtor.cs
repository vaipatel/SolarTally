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
        [ClassData(typeof(StartMoreThanEndData))]
        public void ShouldThrowForStartMoreThanEnd(DateTime start, DateTime end)
        {
            Assert.Throws<TimeIntervalArgumentInvalidException>(() => {
                var ti = new TimeInterval(start, end);
            });
        }

        [Theory]
        [ClassData(typeof(ValidTimeIntervalData))]
        public void ShouldMakeValidTimeInterval(DateTime start, DateTime end,
            DateTime expStart, DateTime expEnd, TimeSpan expDiff)
        {
            var ti = new TimeInterval(start, end);
            Assert.Equal(expStart, ti.Start);
            Assert.Equal(expEnd, ti.End);
            Assert.Equal(expDiff, ti.Difference);
        }

        public class StartMoreThanEndData : TheoryData<DateTime, DateTime>
        {
            public struct StartEndItem
            {
                public DateTime Start { get; set; }
                public DateTime End { get; set; }
            }

            public IEnumerable<StartEndItem> TestData =>
                new List<StartEndItem>()
            {
                // Same Date, Start Time More
                new StartEndItem()
                {
                    Start = new DateTime(2001, 1, 1, 1, 0, 0),
                    End   = new DateTime(2001, 1, 1, 0, 1, 0),
                },
                // Start Date and Time More
                new StartEndItem()
                {
                    Start = new DateTime(2002, 1, 1, 1, 0, 0),
                    End   = new DateTime(2001, 1, 1, 0, 1, 0),
                },
                // Start Date Less, Time More
                new StartEndItem() {
                    Start = new DateTime(2001, 1, 1, 1, 0, 0),
                    End   = new DateTime(2002, 1, 1, 0, 1, 0),
                },
                // Valid Time, but different DateTimeKinds
                new StartEndItem() {
                    Start = new DateTime(2001, 1, 1, 0, 1, 0, DateTimeKind.Local),
                    End   = new DateTime(2001, 1, 1, 1, 0, 0, DateTimeKind.Unspecified)
                }
            };

            public StartMoreThanEndData()
            {
                foreach(var ti in TestData)
                {
                    Add(
                        ti.Start,
                        ti.End
                    );
                }
            }
        }

        public class ValidTimeIntervalData : TheoryData<DateTime, DateTime,
            DateTime, DateTime, TimeSpan>
        {
            public class ValidTimeIntervalItem
            {
                public DateTime Start { get; set; }
                public DateTime End { get; set; }
                public DateTime ExpStart { get; set; }
                public DateTime ExpEnd { get; set; }
                public TimeSpan ExpDiff{ get; set; }
            }

            public IEnumerable<ValidTimeIntervalItem> TestData =>
                new List<ValidTimeIntervalItem>()
            {
                // Same Date, Start Time Less
                // Date will be reset to 2001, 1, 1
                // DateTimeKind will be reset to Unspecified
                // Year, Month, Day are ignored during comparison
                // 1. Hours are both 0
                new ValidTimeIntervalItem()
                {
                    Start    = new DateTime(2002, 3, 14, 0, 0, 0),
                    End      = new DateTime(2002, 3, 14, 0, 1, 0),
                    ExpStart = new DateTime(2001, 1,  1, 0, 0, 0),
                    ExpEnd   = new DateTime(2001, 1,  1, 0, 1, 0),
                    ExpDiff  = new TimeSpan(0, 1, 0)
                },
                // 2. Hours are both 23
                new ValidTimeIntervalItem()
                {
                    Start    = new DateTime(2002, 3, 14, 23, 0, 0),
                    End      = new DateTime(2002, 3, 14, 23, 1, 0),
                    ExpStart = new DateTime(2001, 1,  1, 23, 0, 0),
                    ExpEnd   = new DateTime(2001, 1,  1, 23, 1, 0),
                    ExpDiff  = new TimeSpan(0, 1, 0)
                },
                // 3. Start Date is more, but Time is less by a second
                new ValidTimeIntervalItem()
                {
                    Start    = new DateTime(2001, 3, 14, 23, 0, 59),
                    End      = new DateTime(2002, 3, 14, 23, 1,  0),
                    ExpStart = new DateTime(2001, 1,  1, 23, 0, 59),
                    ExpEnd   = new DateTime(2001, 1,  1, 23, 1,  0),
                    ExpDiff  = new TimeSpan(0, 0, 1)
                },
                // 4. DateTimeKind are same but not Unspecified
                new ValidTimeIntervalItem()
                {
                    Start    = new DateTime(2002, 3, 14, 23, 0, 0, DateTimeKind.Local),
                    End      = new DateTime(2002, 3, 14, 23, 1, 0, DateTimeKind.Local),
                    ExpStart = new DateTime(2001, 1,  1, 23, 0, 0),
                    ExpEnd   = new DateTime(2001, 1,  1, 23, 1, 0),
                    ExpDiff  = new TimeSpan(0, 1, 0)
                },
                new ValidTimeIntervalItem()
                {
                    Start    = new DateTime(2002, 3, 14, 23, 0, 0, DateTimeKind.Utc),
                    End      = new DateTime(2002, 3, 14, 23, 1, 0, DateTimeKind.Utc),
                    ExpStart = new DateTime(2001, 1,  1, 23, 0, 0),
                    ExpEnd   = new DateTime(2001, 1,  1, 23, 1, 0),
                    ExpDiff  = new TimeSpan(0, 1, 0)
                },
                // 5. General 1
                new ValidTimeIntervalItem()
                {
                    Start    = new DateTime(2002, 3, 14,  8, 3, 59, DateTimeKind.Utc),
                    End      = new DateTime(2015, 2, 17, 15, 1,  0, DateTimeKind.Utc),
                    ExpStart = new DateTime(2001, 1,  1,  8, 3, 59),
                    ExpEnd   = new DateTime(2001, 1,  1, 15, 1,  0),
                    ExpDiff  = new TimeSpan(6, 57, 1)
                }
            };

            public ValidTimeIntervalData()
            {
                foreach(var ti in TestData)
                {
                    Add(
                        ti.Start,
                        ti.End,
                        ti.ExpStart,
                        ti.ExpEnd,
                        ti.ExpDiff
                    );
                }
            }
        }
    }
}