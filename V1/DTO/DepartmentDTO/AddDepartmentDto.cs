using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DepartmentsTbl_CRUD.DTO
{
    public class AddDepartmentDto
    {
        [Required(ErrorMessage ="يرجى إدخال اسم القسم")]
        [MaxLength(60)]
        [NameValidation]
        public string DepartmentName { get; set; } 
    }
}
