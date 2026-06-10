using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevetionStudetns.NewFolder
{
    [Table("AcademicYear")]
    public class AcademicYear
    {
        [Key]
        [Required]
        public int  AcademicYearId { get; set; }
        [Required]
        public int AcademicYearLevel {  get; set; }   
    }
}
