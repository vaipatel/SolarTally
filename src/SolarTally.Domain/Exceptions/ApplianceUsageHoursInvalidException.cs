using System;

namespace SolarTally.Domain.Exceptions
{
    public class ApplianceUsageHoursInvalidException : Exception
    {
        public ApplianceUsageHoursInvalidException()
        {
        }

        public ApplianceUsageHoursInvalidException(string message) :
            base(message)
        {
        }

        public ApplianceUsageHoursInvalidException(string message,
            Exception inner) : base(message, inner)
        {
        }
    }
}