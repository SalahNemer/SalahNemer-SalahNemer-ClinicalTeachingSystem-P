using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class NameValidation1Attribute : ValidationAttribute
{
    public NameValidation1Attribute()
    {
        ErrorMessage = "يجب أن يحتوي الاسم على الأحرف العربية أو الإنجليزية أو الأرقام فقط.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string name)
        {
            string pattern = @"^[\p{L}\d\s]+$";

            if (!Regex.IsMatch(name, pattern))
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        return ValidationResult.Success;
    }
}
