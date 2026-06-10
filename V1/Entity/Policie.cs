using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testDtoAndmapper.Entity;

namespace V1.Entity
{
    [Table("Policie")]
    public class Policie
    {
        [Key]
        public int PolicieId { get; set; }
        public  string CreatorId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }
        public string Title { get; set; }
        public string PolicyIdentifier { get; set; }
        public string Objectives { get; set; }  
        public string ExecutionResponsible { get; set; }
        public string Procedures { get; set; }  
        public string Forms { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
