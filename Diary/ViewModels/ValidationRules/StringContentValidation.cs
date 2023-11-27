using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Diary.ViewModels.ValidationRules
{
    public class StringContentValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string? name;
            try
            {
                name = value as string;
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, $"Illegal characters or {ex.Message}");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return new ValidationResult(false, $"Only letters are allowed");
            }

            return ValidationResult.ValidResult;
        }
    }
}
