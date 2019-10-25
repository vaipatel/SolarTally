using System;
using System.Linq;
using System.Collections.Generic;
using SolarTally.Domain.Common;
using SolarTally.Domain.Entities;

// Using the same namespace will make sure your code picks up your 
// extensions no matter where they are in your codebase.
namespace Ardalis.GuardClauses
{
    public static class NoMatchGuard
    {
        public static void NoMatch<T>(this IGuardClause guardClause,
            IEnumerable<T> input, string parameterName, Func<T, bool> pred)
        {
            if (input.Where(pred).Count() == 0)
            {
                throw new ArgumentException($@"The input {parameterName} 
                    does not contain any elements that satisfy the given
                    predicate {pred}", parameterName);
            }
        }

        public static void NoMatch(this IGuardClause guardClause,
            IEnumerable<ApplianceUsage> input, string parameterName,
            Func<ApplianceUsage, bool> pred)
        {
            NoMatch<ApplianceUsage>(guardClause, input, parameterName, pred);
        }
    }
}