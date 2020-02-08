using System;

namespace SolarTally.Domain.Exceptions
{
    public class TimeIntervalArgumentInvalidException : Exception
    {
        public TimeIntervalArgumentInvalidException()
        {
        }

        public TimeIntervalArgumentInvalidException(string message) :
            base(message)
        {
        }

        public TimeIntervalArgumentInvalidException(string message,
            Exception inner) : base(message, inner)
        {
        }
    }
}