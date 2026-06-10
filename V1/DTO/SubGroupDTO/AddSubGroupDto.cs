using DevetionStudetns.DTO.MainGroupDTO;
using System.ComponentModel.DataAnnotations;

namespace DevetionStudetns.DTO.SubGroupDTO
{
    public class AddSubGroupDto 
    {
        [Required(ErrorMessage = "SubGroupSympole is required.")]
        [MaxLength(15)]
        public string SubGroupSympole { get; set; }
        [Required(ErrorMessage = "MainGroupId is required.")]
        [NumbersValidation(ErrorMessage = "يجب أن تحتوي العناصر على أرقام فقط.")]
        public int MainGroupId { get; set; }
    }
}
