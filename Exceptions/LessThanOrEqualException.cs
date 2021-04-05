using System;
using Microsoft.AspNetCore.Http;

namespace Arcaim.Assertor.Exceptions
{
    public class LessThanOrEqualException : ValidationException
    {
        public override string Code => "less_than_or_equal";

        public override int StatusCode => StatusCodes.Status406NotAcceptable;

        private LessThanOrEqualException(string message) : base(message)
        {
        }

        private static LessThanOrEqualException CreateLessThanOrEqualException(string message)
            => new LessThanOrEqualException(message);

        public static LessThanOrEqualException Create()
            => CreateLessThanOrEqualException("An unexpected exception has occurred");

        public static LessThanOrEqualException Create(string message)
            => CreateLessThanOrEqualException(message);

        public static LessThanOrEqualException Create(decimal actual, decimal require)
            => CreateLessThanOrEqualException($"The value requires no more than {require} (actual: {actual})");

        public static LessThanOrEqualException Create(Exception exception)
            => CreateLessThanOrEqualException(exception.Message);
    }
}