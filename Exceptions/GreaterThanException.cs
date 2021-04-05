using Microsoft.AspNetCore.Http;

namespace Arcaim.Assertor.Exceptions
{
    public class GreaterThanException : ValidationException
    {
        public override string Code => "greater_than";

        public override int StatusCode => StatusCodes.Status406NotAcceptable;

        private GreaterThanException(string message) : base(message)
        {
        }

        private static GreaterThanException CreateGreaterThanException(string message)
            => new GreaterThanException(message);

        public static GreaterThanException Create()
            => CreateGreaterThanException("The value is null");

        public static GreaterThanException Create(string message)
            => CreateGreaterThanException(message);

        public static GreaterThanException Create(decimal actual, decimal require)
            => CreateGreaterThanException($"The value requires at least {require} characters (actual: {actual})");
    }
}