using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.DTO.EvaluationFormDto
{
    public class GetEvaluationFormDto
    {
        public int EvaluationFormId { get; set; }
        public string EvaluationFormType { get; set; }
    }
}
