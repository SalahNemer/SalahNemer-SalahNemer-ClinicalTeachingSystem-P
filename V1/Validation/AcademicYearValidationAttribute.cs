using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

public class AcademicYearValidationAttribute : ValidationAttribute
{
    public AcademicYearValidationAttribute()
    {
        ErrorMessage = "يجب إدخال السنة الدراسية بشكل صحيح (YYYY-YYYY) وبشكل منطقي.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("القيمة لا يمكن أن تكون فارغة.");
        }

        string input = value.ToString();

        if (!Regex.IsMatch(input, "^\\d{4}-\\d{4}$"))
        {
            return new ValidationResult("يجب إدخال السنة الدراسية بالشكل الصحيح (YYYY-YYYY). مثال: 2024-2025");
        }

        string[] years = input.Split('-');
        int startYear = int.Parse(years[0]);
        int endYear = int.Parse(years[1]);

        if (endYear != startYear + 1)
        {
            return new ValidationResult("السنة الثانية يجب أن تكون أكبر من السنة الأولى بسنة واحدة فقط.");
        }

        if (startYear < 2020 || startYear > 2030)
        {
            return new ValidationResult("يجب إدخال سنوات دراسية بين 2020 و 2030 فقط.");
        }

        return ValidationResult.Success;
    }
}
