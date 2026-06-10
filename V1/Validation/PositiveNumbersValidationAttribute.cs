using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class PositiveNumbersValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int hours)
        {
            if (hours <= 0 || hours > 250)
            {
                return new ValidationResult("Students hours must be between 0 and 250.");
            }
        }
        else
        {
            return new ValidationResult("Invalid input.");
        }
        return ValidationResult.Success;
    }
}
