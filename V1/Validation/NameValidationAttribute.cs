using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class NameValidationAttribute : ValidationAttribute
{
    public NameValidationAttribute()
    {
        ErrorMessage = "يجب أن يحتوي الاسم على الأحرف العربية أو الإنجليزية فقط.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string name)
        {
            string pattern = @"^[\p{L}\s]+$";

            if (!Regex.IsMatch(name, pattern))
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        return ValidationResult.Success;
    }
}
