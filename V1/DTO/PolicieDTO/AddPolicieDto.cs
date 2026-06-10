using DataBase.Entity;
using System.ComponentModel.DataAnnotations;

namespace V1.DTO.PolicieDTO
{
    public class AddPolicieDto
    {
        [Required(ErrorMessage ="الحقل مطلوب يجب اضافة بيانات ")]
        public string CreatorId { get; set; }
        [Required(ErrorMessage = "الحقل مطلوب يجب اضافة بيانات ")]
        public string Title { get; set; }
        public string PolicyIdentifier { get; set; }
        public string Objectives { get; set; }
        public string ExecutionResponsible { get; set; }
        public string Procedures { get; set; }
        public string Forms { get; set; }
    }
}
