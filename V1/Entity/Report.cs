using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using testDtoAndmapper.Entity;

namespace UploadReportsCode.Entity
{
    public class Report
    {      
        [Key]
        public int ReportId { get; set; }
        [Column("UserId")]
        [MaxLength(20)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }

        [Required]
        [Column("ConcernedParty")]
        [MaxLength(100)]
        public string ConcernedParty { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("ReportName")]
        public string ReportName { get; set; }

        [Required]
        [Column("ReportAttachment")]
        public byte[] ReportAttachment { get; set; }

        [Required]
        [Column("DeliveryDate")]
        public DateTime DeliveryDate { get; set; } = DateTime.Now;
    }
}
