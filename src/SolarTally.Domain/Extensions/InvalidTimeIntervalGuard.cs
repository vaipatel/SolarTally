using System;
using SolarTally.Domain.Exceptions;

// Using the same namespace will make sure your code picks up your 
// extensions no matter where they are in your codebase.
namespace Ardalis.GuardClauses
{
    public static class InvalidTimeIntervalGuard
    {
        public static void InvalidTimeInterval(
            this IGuardClause guardClause, DateTime start, DateTime end)
        {
            // DateTimeKind must be Unspecified
            if (start.Kind != end.Kind)
            {
                throw new TimeIntervalArgumentInvalidException("In TimeInterval, the Start and End must have the same DateTimeKind.");
            }

            // Start time of day must be <= End time of day
            if (start.TimeOfDay > end.TimeOfDay)
            {
                throw new TimeIntervalArgumentInvalidException("In TimeInterval, the Start time of day cannot happen after the End time of day.");
            }
        }
    }
}