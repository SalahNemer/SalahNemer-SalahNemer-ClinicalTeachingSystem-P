using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DevetionStudetns.DTO.RotationsDTO
{
    public class AddRotaionsDto
    {
        [Required(ErrorMessage = "Rotation Name  is required.")]
        public string RotationName { get; set; }
        [Required(ErrorMessage = "Start Rotation  is required.")]
        public DateOnly StartRotationDate { get; set; }
        [Required(ErrorMessage = "End Rotation  is required.")]
        public DateOnly EndRotationDate { get; set; }
        [Required(ErrorMessage = "Academic Year Name is required.")]
        public string AcademicYearName { get; set; }
    }
}
