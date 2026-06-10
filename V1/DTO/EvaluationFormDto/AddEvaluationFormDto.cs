using DevetionStudetns.entitys;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.DTO.EvaluationFormDto
{
    public  class AddEvaluationFormDto
    {
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z\u0600-\u06FF])[A-Za-z\u0600-\u06FF0-9\s.-]+$",
    ErrorMessage = "يُسمح فقط بالأحرف العربية والإنجليزية، ويمكن أن تحتوي على أرقام، وشرطات (-) أو نقاط (.)، ولكن لا يُسمح بالأرقام وحدها.")]
        public string EvaluationFormType { get; set; }
    }
}
