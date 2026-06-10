using DataBase.entitys;
using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevetionStudetns.Entity
{
    [Table("DistributionsMainGroup")]
    public class DistributionsMainGroup
    {
        [Key]
        public int DistributionsMainGroupId { get; set; }
        [Required]
        public int MainGroupId { get; set; }
        [ForeignKey("MainGroupId")]
        public MainGrop MainGroup { get; set; }
        [Required]
        public int RotationId { get; set; }
        [ForeignKey("RotationId")]
        public Rotation Rotations { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
}
