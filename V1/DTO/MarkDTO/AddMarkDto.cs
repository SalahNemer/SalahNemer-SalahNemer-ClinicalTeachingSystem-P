using DataBase.Entity;
using System.ComponentModel.DataAnnotations;

namespace V1.DTO.MarkDTO
{
    public class AddMarkDto
    {
        [Required(ErrorMessage = "This column is required.")]
        public string StudentId { get; set; }
        [Required(ErrorMessage = "This column is required.")]
        public string DoctorId { get; set; }
        [Required(ErrorMessage = "This column is required.")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "This column is required.")]
        public string MarkType { get; set; }
        [Required(ErrorMessage = "This column is required.")]
        [FloatAvarageNumbersValidation]
        public float Mark { get; set; }
        public string? Comments { get; set; }
    }
}
