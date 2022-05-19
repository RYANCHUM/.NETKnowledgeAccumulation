using System;
using System.Globalization;
using System.Windows.Controls;

namespace TimePickerControl.ValidationRule
{
    public class SimpleDateValidationRule : System.Windows.Controls.ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return DateTime.TryParse((value ?? "").ToString(),
                CultureInfo.CurrentCulture,
                DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces,
                out _)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "无效的时间格式");
        }
    }
}