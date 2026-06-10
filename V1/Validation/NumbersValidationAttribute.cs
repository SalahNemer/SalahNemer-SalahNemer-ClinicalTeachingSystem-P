using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class NumbersValidationAttribute : ValidationAttribute
{
    public NumbersValidationAttribute()
    {
        ErrorMessage = "يجب أن تحتوي العناصر على أرقام فقط.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is List<string> list)
        {
            foreach (var item in list)
            {
                if (!Regex.IsMatch(item, @"^\d+$"))
                {
                    return new ValidationResult("يجب أن تحتوي العناصر على أرقام فقط.");
                }
            }
        }
        return ValidationResult.Success;
    }
}
