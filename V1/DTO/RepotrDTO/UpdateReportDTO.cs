using System.ComponentModel.DataAnnotations;

namespace DevetionStudetns.DTO.Repotrs
{
    public class UpdateReportDTO
    {
        [Required(ErrorMessage = "يرجى ادخال اسم التقرير")]
        [MaxLength(100)]
        public string ReportName { get; set; } 
        [Required(ErrorMessage = "يرجى ادخال الجهة المعنية")]
        [MaxLength(100)]
        public string ConcernedParty { get; set; } 
    }
}
