using System;
using System.Collections.Generic;
using SolarTally.Domain.Exceptions;

// Using the same namespace will make sure your code picks up your 
// extensions no matter where they are in your codebase.
namespace Ardalis.GuardClauses
{
    public static class InvalidApplianceUsageHoursOnSolarGuard
    {
        public static void InvalidApplianceUsageHoursOnSolar(
            this IGuardClause guardClause, int numHoursOnSolar,
            string parameterName, int numSolarHours)
        {
            if (numHoursOnSolar > numSolarHours)
            {
                throw new ApplianceUsageHoursInvalidException($"In ApplianceUsage, the number of hours on solar cannot be more than the site's total number of solar hours.");
            }
        }
    }
}