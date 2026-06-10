using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class RoleValedationAttribute : ValidationAttribute
{


    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int roleId)
        {
            if (roleId < 4 || roleId > 6)
            {
                return new ValidationResult("يجب ان تكون السنة الدراسية المدخله ما بين 4 الى 6 ");
            }
        }
        else
        {
            return new ValidationResult("Invalid input.");
        }
        return ValidationResult.Success;
    }
}
