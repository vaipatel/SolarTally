using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using SolarTally.Domain.Common;

namespace SolarTally.Domain.ValueObjects
{
    public class TimeInterval : ValueObject
{
    public TimeSpan Start { get; private set; }

    public TimeSpan End { get; private set; }

    public TimeSpan Difference { get; private set; }

    private TimeInterval() { }

    public TimeInterval(int startHr, int startMin, int endHr, int endMin)
    {
        Guard.Against.InvalidTimeInterval(startHr, startMin, endHr, endMin);
        var start = new TimeSpan(startHr, startMin, 0);
        var end = new TimeSpan(endHr, endMin, 0);
        Start = start;
            End = end;
            Difference = End - Start;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Start;
            yield return End;
            yield return Difference;
        }
    }

    public class TimeIntervalAbrv
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}