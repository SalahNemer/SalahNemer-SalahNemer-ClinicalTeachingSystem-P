using System.ComponentModel.DataAnnotations;

namespace FinalProject.DTO.EvaluationForm_EvaluationQuestionDTO
{
    public class AddEvaluationFormAndEvaluationQuestionDto
    {
        [Required(ErrorMessage = "معرف النموذج رقم يجب ادخال رقم")]
        public int EvaluationFormId { get; set; }
        [Required(ErrorMessage = "معرف السؤال رقم يجب ادخال رقم")]
        public int EvaluationQuestionId { get; set; }
    }
}
