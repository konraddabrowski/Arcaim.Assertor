using System;
using System.Text.RegularExpressions;
using Arcaim.Assertor.Exceptions;
using Arcaim.Assertor.Interfaces;

namespace Arcaim.Assertor.Validators
{
    public class UpperCharExistsValidator : IValidator
    {
        private string _value;

        private UpperCharExistsValidator(string value)
            => _value = value;

        public static UpperCharExistsValidator Create(string value)
            => new UpperCharExistsValidator(value);

        public void Validate()
        {
            bool result;

            try
            {
                result = new Regex(@"[A-Z]+").IsMatch(_value);
            }
            catch (Exception exception)
            {
                throw UpperCharException.Create(exception.Message);
            }

            if (!result)
            {
                throw UpperCharException.Create();
            }
        }
    }
}