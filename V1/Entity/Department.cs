using database.models;
using DevetionStudetns.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testDtoAndmapper.Entity;

namespace DevetionStudetns.NewFolder
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(60)]
        public string DepartmentName { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<Doctor> doctors { get; set; }
        public ICollection<DistributionsMainGroup> DistributionsMainGroup { get; set; }

    }
}
