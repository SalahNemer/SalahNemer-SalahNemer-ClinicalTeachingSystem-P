using System.ComponentModel.DataAnnotations;

namespace V1.DTO.StudentsDTO
{
    public class UpdateStudentAndUserDto
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

        [Required]
        public string UserId { get; set; }

        [Required]
        public string IdNumber { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateOnly DateOfBarth { get; set; }

        [Required]
        public int? RoleId { get; set; }

        [Required]
        public byte? AccountStatus { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
