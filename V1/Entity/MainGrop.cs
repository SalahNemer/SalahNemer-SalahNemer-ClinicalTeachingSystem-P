using DevetionStudetns.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevetionStudetns.NewFolder
{
    [Table("MainGroup")]
    public class MainGrop
    {
        [Key]
        public int MainGroupId { get; set; }
        [Required]
        public string MainGroupSympole { get; set; }
        [Required]
        public int AcademicYearId { get; set; }
        [Required]
        public string AcademicYearName { get; set; }
        public ICollection<SubGroup> SubGroups { get; set; }
        public ICollection<DistributionsMainGroup> DistributionsMainGroup { get; set; }
    }
}
