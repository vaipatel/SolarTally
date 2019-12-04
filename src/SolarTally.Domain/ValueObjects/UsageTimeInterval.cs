using System.Collections.Generic;
using SolarTally.Domain.Common;
using SolarTally.Domain.Enumerations;

namespace SolarTally.Domain.ValueObjects
{
    public class UsageTimeInterval : ValueObject
    {
        public TimeInterval TimeInterval { get; private set; }
        public UsageKind UsageKind { get; private set; }

        private UsageTimeInterval()
        {}

        public UsageTimeInterval(TimeInterval timeInterval,
            UsageKind usageKind = UsageKind.UsingSolar)
        {
            TimeInterval = timeInterval;
            UsageKind = usageKind;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return TimeInterval;
            yield return UsageKind;
        }
    }

    public class UsageTimeIntervalAbrv
    {
        public TimeIntervalAbrv TimeInterval { get; set; }
        public UsageKind UsageKind { get; set; }
    }
}