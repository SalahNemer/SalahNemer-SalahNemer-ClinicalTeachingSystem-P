using System.ComponentModel.DataAnnotations;

namespace UploadReportsCode.DTO
{
    public class AddReportsDto
    {
        [Required(ErrorMessage = "يرجى ادخال معرف المستخدم")]
        [MaxLength(20)]
        public string UserId { get; set; }
        [Required(ErrorMessage = "يرجى إدخال اسم التقرير")]
        [MaxLength(100)]
        public string ReportName { get; set; } 
        [Required(ErrorMessage = "يرجى إدخال الجهة المعنية")]
        [MaxLength(100)]
        public string ConcernedParty { get; set; } 
        [Required(ErrorMessage = "يرجى إدخال ملف التقرير")]
        public IFormFile ReportAttachment { get; set; } 
    }
}
