using System;
using System.Text.RegularExpressions;
using Arcaim.Assertor.Exceptions;
using Arcaim.Assertor.Interfaces;

namespace Arcaim.Assertor.Validators
{
    public class DigitExistsValidator : IValidator
    {
        private string _value;

        private DigitExistsValidator(string value) => _value = value;

        public static DigitExistsValidator Create(string value)
            => new DigitExistsValidator(value);

        public void Validate()
        {
            bool result;

            try
            {
                result = new Regex(@"[0-9]+").IsMatch(_value);
            }
            catch (Exception exception)
            {
                throw DigitException.Create(exception.Message);
            }

            if (!result)
            {
                throw DigitException.Create();
            }
        }
    }
}