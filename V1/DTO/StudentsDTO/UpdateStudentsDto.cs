using System.ComponentModel.DataAnnotations;

namespace FinalProject.DTO.StudentsDTO
{
    public class UpdateStudentsDto
    { 
        [Required(ErrorMessage = "This column is required.")]
        [YearNameValedation]
        public string YearEnrollment { get; set; }
        [Required(ErrorMessage = "This column is required.")]
        [AverageNumbersValidation]
        public double CumulativeAverage { get; set; }
        [Required(ErrorMessage = "This column is required.")]
        [NumbersValidation]
        [RoleValedation]
        public int StudentLevel { get; set; }
    }
}
