using System.ComponentModel.DataAnnotations;

namespace DevetionStudetns.DTO.TADTO
{
    public class AddTADto
    {
        [Required(ErrorMessage = "يرجى ادخال معرف المستخدم")]
        [MaxLength(20)]
        public string TAId { get; set; }
        [Required(ErrorMessage = "يرجى ادخال السنة المشرف عليها")]
        public int SupervisedYear { get; set; }
    }
}
