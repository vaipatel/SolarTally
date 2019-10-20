using System;
using System.Linq;
using System.Collections.Generic;
using SolarTally.Domain.Common;
using SolarTally.Domain.Entities;

// Using the same namespace will make sure your code picks up your 
// extensions no matter where they are in your codebase.
namespace Ardalis.GuardClauses
{
    public static class AlreadyHasOneGuard
    {
        public static void AlreadyHasOne<T>(this IGuardClause guardClause,
            IEnumerable<T> input, string parameterName, Func<T, bool> pred)
        {
            if (input.Where(pred).Count() > 0)
            {
                throw new ArgumentException($@"The input {parameterName} 
                    alreadys contains elements that satisfy the given
                    predicate", parameterName);
            }
        }

        public static void AlreadyHasOne(this IGuardClause guardClause,
            IEnumerable<ApplianceUsage> input, string parameterName,
            Func<ApplianceUsage, bool> pred)
        {
            AlreadyHasOne<ApplianceUsage>(guardClause, input, parameterName,
                pred);
        }
    }
}