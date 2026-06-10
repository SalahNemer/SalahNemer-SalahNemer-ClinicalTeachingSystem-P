using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testDtoAndmapper.Entity;

namespace DataBase.entitys
{
    [Table("Divisions")]
    public class Division
    {
        [Key]
        public int DivisionId { get; set; }
        [Required]
        [Column("SubGroupId")]
        public int SubGroupId { get; set; }
        [ForeignKey("SubGroupId")]
        public SubGroup SubGroup { get; set; }
        [Required]
        [Column("StudentId")]
        [MaxLength(20)]
        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Users Student { get; set; }    
        [Column("DivisionStatus")]
        public int? DivisionStatus {  get; set; }
        public string? Notes { get; set; }
    }
}
