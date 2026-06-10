using System.ComponentModel.DataAnnotations;

namespace DevetionStudetns.DTO.MainGroupDTO
{
    public class AddMainGroupDto
    {
        [Required(ErrorMessage = "MainGroup is required.")]
        public string MainGroupSympole { get; set; }
        [Required(ErrorMessage = "AcademicYearId is required.")]
        public int AcademicYearId { get; set; }
        [Required(ErrorMessage = "AcademicYearName is required.")]
        public string AcademicYearName { get; set; }
    }
}
