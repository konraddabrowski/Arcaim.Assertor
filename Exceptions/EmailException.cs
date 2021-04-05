using System;
using Microsoft.AspNetCore.Http;

namespace Arcaim.Assertor.Exceptions
{
    public class EmailException : ValidationException
    {
        public override string Code => "invalid_email";

        public override int StatusCode => StatusCodes.Status403Forbidden;

        private EmailException(string message) : base(message)
        {
        }

        private static EmailException CreateEmailException(string message)
            => new EmailException(message);

        public static EmailException Create(string email)
            => CreateEmailException($"The email \\\"{email}\\\" is incorrect");

        public static EmailException Create(Exception exception)
            => CreateEmailException(exception.Message);
    }
}