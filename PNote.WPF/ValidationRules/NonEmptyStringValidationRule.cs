
using System.Globalization;
using System.Windows.Controls;

namespace PNote.ValidationRules
{
    public class NonEmptyStringValidationRule : ValidationRule
    {
        public string? FieldName { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string username = (string)value;

            if (username == null)
                return new ValidationResult(false, $"{FieldName} is not a string.");

            if (string.IsNullOrEmpty(username))
                return new ValidationResult(false, $"{FieldName} cannot be empty");

            return ValidationResult.ValidResult;
        }
    }
}
