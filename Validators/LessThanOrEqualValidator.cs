using Arcaim.Assertor.Exceptions;
using Arcaim.Assertor.Interfaces;

namespace Arcaim.Assertor.Validators
{
    public class LessThanOrEqualValidator : IValidator
    {
        private decimal? _value;
        private decimal _valueToCompare;

        private LessThanOrEqualValidator(decimal? value, decimal valueToCompare)
            => (_value, _valueToCompare) = (value, valueToCompare);

        public static LessThanOrEqualValidator Create(decimal? value, decimal valueToCompare)
            => new LessThanOrEqualValidator(value, valueToCompare);

        public void Validate()
        {
            if (!_value.HasValue)
            {
                throw LessThanException.Create();
            }

            if (_value <= _valueToCompare)
            {
                throw LessThanException.Create(_value.Value, _valueToCompare);
            }
        }
    }
}