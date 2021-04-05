using System;
using Microsoft.AspNetCore.Http;

namespace Arcaim.Assertor.Exceptions
{
    public class MaximumLengthException : ValidationException
    {
        public override string Code => "maximum_length";

        public override int StatusCode => StatusCodes.Status406NotAcceptable;

        private MaximumLengthException(string message) : base(message)
        {
        }

        private static MaximumLengthException CreateMaximumLengthException(string message)
            => new MaximumLengthException(message);

        public static MaximumLengthException Create(string message)
            => CreateMaximumLengthException(message);

        public static MaximumLengthException Create(int actual, int require)
            => CreateMaximumLengthException($"The value requires no more than {require} characters (actual: {actual})");

        public static MaximumLengthException Create(Exception exception)
            => CreateMaximumLengthException(exception.Message);
    }
}