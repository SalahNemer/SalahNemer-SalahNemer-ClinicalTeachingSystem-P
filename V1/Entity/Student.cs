using BuildDB_Team.entitys;
using DataBase.Entity;
using database.models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testDtoAndmapper.Entity
{
    [Table("Students")]
    public class Student
    {
        [Key]
        [Column("UserId")]
        [MaxLength(20)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }
        [Required]
        [MaxLength(20)]
        [Column("YearEnrollment")]
        public string YearEnrollment { get; set; }
       
        [Required]
        [Column("CumulativeAverage")]
        public double CumulativeAverage { get; set; }

        [Required]
        [Column("StudentLevel")]
        public int StudentLevel { get; set; }
        public ICollection<Marks> Marks { get; set; }
        public ICollection<WeeklyEvaluation> WeeklyEvaluation { get; set; }
    }
}
