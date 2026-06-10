using System.ComponentModel.DataAnnotations;

namespace DevetionStudetns.DTO.TADTO
{
    public class UpdateTaDto
    {
        [Required(ErrorMessage = "يرجى ادخال السنة المشرف عليها")]
        public int SupervisedYear { get; set; }
    }
}
