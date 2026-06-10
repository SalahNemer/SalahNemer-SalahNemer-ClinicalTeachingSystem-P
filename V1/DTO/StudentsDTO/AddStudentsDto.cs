using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using testDtoAndmapper.Entity;

namespace DevetionStudetns.DTO.StudentsDTO
{
    public class StudentsAddDto
    {
        [Required(ErrorMessage = "This column is required.")]
        public string UserId { get; set; }
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
