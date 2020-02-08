using System;
using SolarTally.Domain.Exceptions;

// Using the same namespace will make sure your code picks up your 
// extensions no matter where they are in your codebase.
namespace Ardalis.GuardClauses
{
    public static class InvalidTimeIntervalGuard
    {
        public static void InvalidTimeInterval(
            this IGuardClause guardClause, int startHr, int startMin, int endHr,
            int endMin
        )
        {
            // hr must be in [0,23], min must be in [0,59]
            int hrLB = 0, hrUB = 23, minLB = 0, minUB = 59;
            if (startHr < hrLB || startHr > hrUB ||
                endHr   < hrLB || endHr   > hrUB)
            {
                throw new TimeIntervalArgumentInvalidException($"In TimeInterval, the hour value must be between {hrLB} and {hrUB} (inclusive).");
            }
            if (startMin < minLB || startMin > minUB ||
                endMin   < minLB || endMin   > minUB)
            {
                throw new TimeIntervalArgumentInvalidException($"In TimeInterval, the minute value must be between {minLB} and {minUB} (inclusive).");
            }

            var start = new TimeSpan(startHr, startMin, 0);
            var end   = new TimeSpan(endHr,   endMin,   0);
            // Start timespan must be <= End timespan
            if (start > end)
            {
                throw new TimeIntervalArgumentInvalidException("In TimeInterval, the start time must not come after the end time.");
            }
        }
    }
}