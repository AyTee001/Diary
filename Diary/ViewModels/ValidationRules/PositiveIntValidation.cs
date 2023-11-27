using System;
using System.Globalization;
using System.Windows.Controls;

namespace Diary.ViewModels.ValidationRules
{
    public class PositiveIntValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int res;
            try
            {
                res = int.Parse((string)value);
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, $"Not an integer: {ex.Message}");
            }
            if(res <= 0)
            {
                return new ValidationResult(false, "Only positive integers allowed");
            }
            return ValidationResult.ValidResult;
        }
    }
}
