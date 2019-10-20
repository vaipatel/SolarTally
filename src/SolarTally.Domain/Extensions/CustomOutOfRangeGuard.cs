using System;
using System.Collections.Generic;

// Using the same namespace will make sure your code picks up your 
// extensions no matter where they are in your codebase.
namespace Ardalis.GuardClauses
{
    public static class CustomOutOfRangeGuard
    {
        public static void CustomOutOfRange<T>(this IGuardClause guardClause,
            T input, string parameterName, T lowerBound, T upperBound)
        {
            Comparer<T> comparer = Comparer<T>.Default;
            
            if (comparer.Compare(input, lowerBound) < 0 ||
                comparer.Compare(input, upperBound) > 0)
            {
                // use the $@ syntax - https://stackoverflow.com/a/31766560
                throw new ArgumentOutOfRangeException(parameterName,
                    $@"Input {parameterName} was outside the allowed bounds.");
            }
        }

        public static void CustomOutOfRange(this IGuardClause guardClause,
            decimal input, string parameterName, decimal lowerBound,
            decimal upperBound)
        {
            CustomOutOfRange<decimal>(guardClause, input, parameterName,
                lowerBound, upperBound);
        }

        public static void CustomOutOfRange(this IGuardClause guardClause,
            float input, string parameterName, float lowerBound,
            float upperBound)
        {
            CustomOutOfRange<float>(guardClause, input, parameterName,
                lowerBound, upperBound);
        }

        public static void CustomOutOfRange(this IGuardClause guardClause,
            double input, string parameterName, double lowerBound,
            double upperBound)
        {
            CustomOutOfRange<double>(guardClause, input, parameterName,
                lowerBound, upperBound);
        }
    }
}