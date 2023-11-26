using System;

namespace Entities.Exceptions
{
    public class MaxAgeRangeException : Exception
    {
        public MaxAgeRangeException() : base($"Max age can't be less than min age.")
        {
        }
    }
}
