using Microsoft.AspNetCore.Http;

namespace Arcaim.Assertor.Exceptions
{
    public class GreaterThanOrEqualException : ValidationException
    {
        public override string Code => "greater_than_or_equal";

        public override int StatusCode => StatusCodes.Status406NotAcceptable;

        private GreaterThanOrEqualException(string message) : base(message)
        {
        }

        private static GreaterThanOrEqualException CreateGreaterThanOrEqualException(string message)
            => new GreaterThanOrEqualException(message);

        public static GreaterThanOrEqualException Create()
            => CreateGreaterThanOrEqualException("The value is null");

        public static GreaterThanOrEqualException Create(string message)
            => CreateGreaterThanOrEqualException(message);

        public static GreaterThanOrEqualException Create(decimal actual, decimal require)
            => CreateGreaterThanOrEqualException($"The value requires at least {require} characters (actual: {actual})");
    }
}