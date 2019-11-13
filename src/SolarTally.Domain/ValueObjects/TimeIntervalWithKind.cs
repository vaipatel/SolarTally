using System.Collections.Generic;
using SolarTally.Domain.Common;
using SolarTally.Domain.Enumerations;

namespace SolarTally.Domain.ValueObjects
{
    public class TimeIntervalWithKind : ValueObject
    {
        public TimeInterval TimeInterval { get; private set; }
        public TimeIntervalKind TimeIntervalKind { get; private set; }

        private TimeIntervalWithKind()
        {}

        public TimeIntervalWithKind(TimeInterval timeInterval,
            TimeIntervalKind timeIntervalKind = TimeIntervalKind.UsingSolar)
        {
            TimeInterval = timeInterval;
            TimeIntervalKind = timeIntervalKind;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return TimeInterval;
            yield return TimeIntervalKind;
        }
    }
}