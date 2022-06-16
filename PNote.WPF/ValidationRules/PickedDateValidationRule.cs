using System;
using System.Globalization;
using System.Windows.Controls;

namespace PNote.ValidationRules
{
    public class PickedDateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var date = (DateTime?)value;

            if (date == null)
                return new ValidationResult(false, "No date set.");

            if (date < DateTime.Now)
                return new ValidationResult(false, "Can't set date in past.");

            return ValidationResult.ValidResult;
        }
    }
}
