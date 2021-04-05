using Microsoft.AspNetCore.Http;

namespace Arcaim.Assertor.Exceptions
{
    public class SmallCharException : ValidationException
    {
        public override string Code => "small_char";

        public override int StatusCode => StatusCodes.Status406NotAcceptable;

        private SmallCharException(string message) : base(message)
        {
        }

        private static SmallCharException CreateSmallCharException(string message)
            => new SmallCharException(message);

        public static SmallCharException Create()
            => CreateSmallCharException("The value requires at least one small char");

        public static SmallCharException Create(string message)
            => CreateSmallCharException(message);
    }
}