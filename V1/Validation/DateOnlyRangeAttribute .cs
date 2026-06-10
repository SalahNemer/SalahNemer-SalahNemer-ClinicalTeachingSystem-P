using System;
using System.ComponentModel.DataAnnotations;

public class DateOnlyRangeAttribute : ValidationAttribute
{
    private readonly DateOnly _minDate;
    private readonly DateOnly _maxDate;

    public DateOnlyRangeAttribute(string minDate, string maxDate)
    {
        _minDate = DateOnly.Parse(minDate);
        _maxDate = DateOnly.Parse(maxDate);
        ErrorMessage = $"تاريخ الميلاد يجب أن يكون بين {_minDate} و {_maxDate}.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateOnly dateOfBirth)
        {
            if (dateOfBirth < _minDate || dateOfBirth > _maxDate)
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        return ValidationResult.Success;
    }
}
