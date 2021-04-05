using System;
using Microsoft.AspNetCore.Http;

namespace Arcaim.Assertor.Exceptions
{
    public class MinimumLengthException : ValidationException
    {
        public override string Code => "minimum_length";

        public override int StatusCode => StatusCodes.Status406NotAcceptable;

        private MinimumLengthException(string message) : base(message)
        {
        }

        private static MinimumLengthException CreateMinimumLengthException(string message)
            => new MinimumLengthException(message);

        public static MinimumLengthException Create(string message)
            => CreateMinimumLengthException(message);

        public static MinimumLengthException Create(int actual, int require)
            => CreateMinimumLengthException($"The value requires no more than {require} characters (actual: {actual})");

        public static MinimumLengthException Create(Exception exception)
            => CreateMinimumLengthException(exception.Message);
    }
}