using System;
using System.Net.Mail;
using Arcaim.Assertor.Exceptions;
using Arcaim.Assertor.Interfaces;

namespace Arcaim.Assertor.Validators
{
    public class EmailValidator : IValidator
    {
        private string _email;

        private EmailValidator(string email) => _email = email;

        public static EmailValidator Create(string email)
            => new EmailValidator(email);

        public void Validate()
        {
            MailAddress mailAddress;

            try
            {
                mailAddress = new(_email);
            }
            catch (Exception exception)
            {
                throw EmailException.Create(exception);
            }

            if (mailAddress.Address != _email)
            {
                throw EmailException.Create(_email);
            }
        }
    }
}
