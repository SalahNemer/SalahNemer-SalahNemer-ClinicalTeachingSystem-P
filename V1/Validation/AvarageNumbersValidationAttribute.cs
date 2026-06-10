using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class AverageNumbersValidationAttribute : ValidationAttribute
{

    public AverageNumbersValidationAttribute()
    {
        ErrorMessage = "يجب أن تكون العلامة بين 0 و 100.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is double grade) 
        {
            if (grade <= 0.0 || grade >= 100.0)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
        else if (value is int intGrade)
        {
            if (intGrade <= 0 || intGrade > 100)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }

        return new ValidationResult("نوع البيانات المدخلة غير صالح. يجب إدخال رقم بين 0 و 100.");
    }
}


