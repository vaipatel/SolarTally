using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using SolarTally.Domain.Common;

namespace SolarTally.Domain.ValueObjects
{
    public class TimeInterval : ValueObject
{
    public DateTime Start { get; private set; }

    public DateTime End { get; private set; }

    public TimeSpan Difference { get; private set; }

    private TimeInterval() { }

    public TimeInterval(DateTime start, DateTime end)
    {
        Guard.Against.InvalidTimeInterval(start, end);
        Start = 
            new DateTime(2001, 1, 1, start.Hour, start.Minute, start.Second);
        End = 
            new DateTime(2001, 1, 1, end.Hour, end.Minute, end.Second);
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
}