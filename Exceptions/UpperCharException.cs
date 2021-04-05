using Microsoft.AspNetCore.Http;

namespace Arcaim.Assertor.Exceptions
{
    public class UpperCharException : ValidationException
    {
        public override string Code => "upper_char";

        public override int StatusCode => StatusCodes.Status406NotAcceptable;

        private UpperCharException(string message) : base(message)
        {
        }

        private static UpperCharException CreateUpperCharException(string message)
            => new UpperCharException(message);

        public static UpperCharException Create()
            => CreateUpperCharException("The value requires at least one upper char");

        public static UpperCharException Create(string message)
            => CreateUpperCharException(message);
    }
}