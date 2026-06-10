using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testDtoAndmapper.Entity;

namespace DataBase.Entity
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("SenderId")]
        public string SenderId { get; set; }
        [ForeignKey("SenderId")]
        public Users Sender { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("ReseverId")]
        public string ReseverId { get; set; }
        [ForeignKey("ReseverId")]
        public Users Resever { get; set; }

        [Required]
        [Column("Contant")]
        public string Contant { get;set; }

        [Required]
        [Column("DateSend")]
        public DateTime DateSend { get; set; } = DateTime.Now;
    }
}
