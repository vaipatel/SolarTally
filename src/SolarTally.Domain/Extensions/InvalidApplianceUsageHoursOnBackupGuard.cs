using System;
using System.Collections.Generic;
using SolarTally.Domain.Exceptions;

// Using the same namespace will make sure your code picks up your 
// extensions no matter where they are in your codebase.
namespace Ardalis.GuardClauses
{
    public static class InvalidApplianceUsageHoursOnBackupGuard
    {
        public static void InvalidApplianceUsageHoursOnBackup(
            this IGuardClause guardClause, int numHoursOnBackup,
            string parameterName, int numHoursOnSolar)
        {
            if (numHoursOnBackup + numHoursOnSolar > 24)
            {
                throw new ApplianceUsageHoursInvalidException($"In ApplianceUsage, the total number of hours i.e hrs on solar + hrs on backup, cannot be more than 24.");
            }
        }
    }
}