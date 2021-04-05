using System;
using System.Text.RegularExpressions;
using Arcaim.Assertor.Exceptions;
using Arcaim.Assertor.Interfaces;

namespace Arcaim.Assertor.Validators
{
    public class MinimumLengthValidator : IValidator
    {
        private string _value;
        private int _minimumValue;

        private MinimumLengthValidator(string value, int minimumValue)
            => (_value, _minimumValue) = (value, minimumValue);

        public static MinimumLengthValidator Create(string value, int minimumValue)
            => new MinimumLengthValidator(value, minimumValue);

        public void Validate()
        {
            bool result;

            try
            {
                result = new Regex(@".{" + _minimumValue + ",}").IsMatch(_value);
            }
            catch (Exception exception)
            {
                throw MinimumLengthException.Create(exception.Message);
            }

            if (!result)
            {
                throw MinimumLengthException.Create(_value.Length, _minimumValue);
            }
        }
    }
}