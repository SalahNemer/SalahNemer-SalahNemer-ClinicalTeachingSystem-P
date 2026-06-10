using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testDtoAndmapper.Entity;

namespace DataBase.Entity
{
    [Table("TA")]
    public class TA
    {
        [Key]
        [MaxLength(20)]
        [Column("TAId")]
        public string TAId { get; set; }
        [ForeignKey("TAId")]
        public Users users { get; set; }
        [Required]
        [Column("SupervisedYear")]
        public int SupervisedYear { get; set; }
    }
}
