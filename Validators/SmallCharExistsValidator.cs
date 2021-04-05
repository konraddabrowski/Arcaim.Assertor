using System;
using System.Text.RegularExpressions;
using Arcaim.Assertor.Exceptions;
using Arcaim.Assertor.Interfaces;

namespace Arcaim.Assertor.Validators
{
    public class SmallCharExistsValidator : IValidator
    {
        private string _value;

        private SmallCharExistsValidator(string value)
            => _value = value;

        public static SmallCharExistsValidator Create(string value)
            => new SmallCharExistsValidator(value);

        public void Validate()
        {
            bool result;

            try
            {
                result = new Regex(@"[a-z]+").IsMatch(_value);
            }
            catch (Exception exception)
            {
                throw SmallCharException.Create(exception.Message);
            }

            if (!result)
            {
                throw SmallCharException.Create();
            }
        }
    }
}