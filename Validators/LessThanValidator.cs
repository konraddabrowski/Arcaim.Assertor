using Arcaim.Assertor.Exceptions;
using Arcaim.Assertor.Interfaces;

namespace Arcaim.Assertor.Validators
{
    public class LessThanValidator : IValidator
    {
        private decimal? _value;
        private decimal _valueToCompare;

        private LessThanValidator(decimal? value, decimal valueToCompare)
            => (_value, _valueToCompare) = (value, valueToCompare);

        public static LessThanValidator Create(decimal? value, decimal valueToCompare)
            => new LessThanValidator(value, valueToCompare);

        public void Validate()
        {
            if (!_value.HasValue)
            {
                throw LessThanException.Create();
            }

            if (_value < _valueToCompare)
            {
                throw LessThanException.Create(_value.Value, _valueToCompare);
            }
        }
    }
}