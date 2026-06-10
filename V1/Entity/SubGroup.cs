using DataBase.entitys;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevetionStudetns.NewFolder
{
    [Table("SubGroup")]
    public class SubGroup
    {
        [Key]
        public int SubGroupId { get; set; }
        [Required]
        public string SubGroupSympole { get; set; }
        [Required]
        public int MainGroupId { get; set; }
        [ForeignKey("MainGroupId")]
        public MainGrop MainGrop { get; set; }
        public int? NumberOfStudetns { get; set; }
        public ICollection<Distribution> Distributions { get; set; }
        public ICollection<Division> divisions { get; set; }
    }
}
