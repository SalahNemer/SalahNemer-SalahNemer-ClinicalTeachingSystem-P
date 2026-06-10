using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class YearNameValedationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string hour)
        {
            if (int.TryParse(hour, out int hours))
            {
                if (hours < 2015 || hours > 2030)
                {
                    return new ValidationResult("يجب على السنة المدخلة أن تكون ما بين 2015 إلى 2030");
                }
            }
            else
            {
                return new ValidationResult("يجب إدخال رقم صالح.");
            }
        }
        else
        {
            return new ValidationResult("المدخل غير صالح.");
        }

        return ValidationResult.Success;
    }
}
