using System.ComponentModel.DataAnnotations;
namespace V1.abed.AllAcademicYearDTO
{
    public class AddAllAcademicYearDto
    {
        [Required(ErrorMessage = "The Column is required.")]
        [AcademicYearValidation]
        public string CurrentAcademicYearName { set; get; }
    }
}
