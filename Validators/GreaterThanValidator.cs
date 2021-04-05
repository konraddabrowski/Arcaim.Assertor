using Arcaim.Assertor.Exceptions;
using Arcaim.Assertor.Interfaces;

namespace Arcaim.Assertor.Validators
{
    public class GreaterThanValidator : IValidator
    {
        private decimal? _value;
        private decimal _valueToCompare;

        private GreaterThanValidator(decimal? value, decimal valueToCompare)
            => (_value, _valueToCompare) = (value, valueToCompare);

        public static GreaterThanValidator Create(decimal? value, decimal valueToCompare)
            => new GreaterThanValidator(value, valueToCompare);

        public void Validate()
        {
            if (!_value.HasValue)
            {
                throw GreaterThanException.Create();
            }

            if (_value > _valueToCompare)
            {
                throw GreaterThanException.Create(_value.Value, _valueToCompare);
            }
        }
    }
}