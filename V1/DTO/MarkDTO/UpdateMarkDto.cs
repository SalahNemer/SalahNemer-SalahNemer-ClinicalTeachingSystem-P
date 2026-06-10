using System.ComponentModel.DataAnnotations;

namespace V1.DTO.MarkDTO
{
    public class UpdateMarkDto
    {
        [Required(ErrorMessage = "This column is required.")]
        public string StudentId { get; set; }
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
