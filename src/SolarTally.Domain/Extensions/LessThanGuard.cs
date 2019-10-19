using System;
using System.Collections.Generic;

// Using the same namespace will make sure your code picks up your 
// extensions no matter where they are in your codebase.
namespace Ardalis.GuardClauses
{
    public static class LessThanGuard
    {
        public static void LessThan<T>(this IGuardClause guardClause, T input, 
                                       string parameterName, T lowerBound)
        {
            Comparer<T> comparer = Comparer<T>.Default;
            
            if (comparer.Compare(input, lowerBound) < 0)
            {
                // use the $@ syntax - https://stackoverflow.com/a/31766560
                throw new ArgumentOutOfRangeException($@"Input {parameterName} 
                    was lower than the allowed lower bound.", parameterName);
            }
        }

        public static void LessThan(this IGuardClause guardClause, int input,
                                    string parameterName, int lowerBound)
        {
            LessThan<int>(guardClause, input, parameterName, lowerBound);
        }
    }
}