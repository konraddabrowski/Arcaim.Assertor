using Microsoft.AspNetCore.Http;

namespace Arcaim.Assertor.Exceptions
{
    public class DigitException : ValidationException
    {
        public override string Code => "digit_value";

        public override int StatusCode => StatusCodes.Status406NotAcceptable;

        private DigitException(string message) : base(message)
        {
        }

        public static DigitException CreateDigitException(string message)
            => new DigitException(message);

        public static DigitException Create()
            => CreateDigitException("The value requires at least one digit number");

        public static DigitException Create(string message)
            => CreateDigitException(message);
    }
}