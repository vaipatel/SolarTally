using System;
using System.Collections.Generic;
using SolarTally.Domain.Exceptions;

// Using the same namespace will make sure your code picks up your 
// extensions no matter where they are in your codebase.
namespace Ardalis.GuardClauses
{
    public static class InvalidApplianceUsageHoursGuard
    {
        public static void InvalidApplianceUsageHours(
            this IGuardClause guardClause, int numHours,
            decimal percentHrsOnSolar, int numSolarHours)
        {
            if (numHours * percentHrsOnSolar > numSolarHours)
            {
                throw new ApplianceUsageHoursInvalidException($@"In 
                    ApplianceUsage, the product of the number of hours times 
                    percent hours on solar cannot be more than the site's total 
                    number of solar hours.");
            }
        }
    }
}