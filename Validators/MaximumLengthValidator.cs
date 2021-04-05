using System;
using System.Text.RegularExpressions;
using Arcaim.Assertor.Exceptions;
using Arcaim.Assertor.Interfaces;

namespace Arcaim.Assertor.Validators
{
    public class MaximumLengthValidator : IValidator
    {
        private string _value;
        private int _maximumValue;

        private MaximumLengthValidator(string value, int maximumValue)
            => (_value, _maximumValue) = (value, maximumValue);

        public static MaximumLengthValidator Create(string value, int maximumValue)
            => new MaximumLengthValidator(value, maximumValue);

        public void Validate()
        {
            bool result;

            try
            {
                result = new Regex(@".{," + _maximumValue + "}").IsMatch(_value);
            }
            catch (Exception exception)
            {
                throw MaximumLengthException.Create(exception.Message);
            }

            if (!result)
            {
                throw MaximumLengthException.Create(_value.Length, _maximumValue);
            }
        }
    }
}