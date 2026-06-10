using DataBase.entitys;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace database.models
{
    [Table("Hospitals")]
    public class Hospital
    {
        [Key]
        [Required]
        [Column("HospitalId")]
        public int HospitalId { get; set; }
        [Required]
        [MaxLength(100)]
        [Column("HospitalName")]
        public string HospitalName { get; set; }
        [Required]
        [MaxLength(200)]
        [Column("Location")]
        public string Location { get; set; }
        [Required]
        [MaxLength(20)]
        [Column("ContactNumber")]
        public string ContactNumber { get; set; }
        [Required]
        [Column("HospitalCapacity")]
        public int HospitalCapacity { get; set; }

        public ICollection<Doctor> doctors { get; set; }
    }
}
