using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace V1.Entity
{
    [Table("AllAcademicYear")]
    public class AllAcademinYears
    {
        [Key]
        public int CurrentAcademicYearId { set; get; }
        public string CurrentAcademicYearName { set; get; }
    }
}
