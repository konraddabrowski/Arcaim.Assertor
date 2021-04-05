using System;
using Microsoft.AspNetCore.Http;

namespace Arcaim.Assertor.Exceptions
{
    public class LessThanException : ValidationException
    {
        public override string Code => "less_than";

        public override int StatusCode => StatusCodes.Status406NotAcceptable;

        private LessThanException(string message) : base(message)
        {
        }

        private static LessThanException CreateLessThanException(string message)
            => new LessThanException(message);

        public static LessThanException Create()
            => CreateLessThanException("An unexpected exception has occurred");

        public static LessThanException Create(string message)
            => CreateLessThanException(message);

        public static LessThanException Create(decimal actual, decimal require)
            => CreateLessThanException($"The value requires no more than {require} (actual: {actual})");

        public static LessThanException Create(Exception exception)
            => CreateLessThanException(exception.Message);
    }
}