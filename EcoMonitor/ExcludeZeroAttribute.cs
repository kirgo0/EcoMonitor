using System.ComponentModel.DataAnnotations;

namespace EcoMonitor
{

    public class ExcludeZeroAttribute : ValidationAttribute
    {
        private readonly double _maxValue;

        public ExcludeZeroAttribute(double maxValue)
        {
            _maxValue = maxValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !(value is double))
            {
                return new ValidationResult("The value is not a valid number.");
            }

            double doubleValue = (double)value;
            if (doubleValue <= 0 || doubleValue > _maxValue)
            {
                return new ValidationResult($"The value must be greater than 0 and not more than {_maxValue}.");
            }

            return ValidationResult.Success;
        }


    }
}
