using BuildDB_Team.entitys;
using DataBase.Entity;
using DevetionStudetns.Entity;
using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testDtoAndmapper.Entity;

namespace database.models
{
    [Table("Doctors")]
    public class Doctor
    {
        [Key]
        [Required]
        [MaxLength(20)]
        [Column("UserId")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Users users { get; set; }

        [Required]
        [Column("HospitalId")]
        public int HospitalId { get; set; }
        [ForeignKey("HospitalId")]
        public Hospital hospital { get; set; }

        [Required]
        [Column("DepartmentId")]
        public int DepartmentID { get; set; }
        [ForeignKey("DepartmentID")]
        public Department department { get; set; }
        
        [Required]
        [MaxLength(100)]
        [Column("MedicalSpecialty")]
        public string MedicalSpecialty { get; set; }
        [Required]
        public string AcademicDegree { get; set; }
        [Required]
        public string YearOfObtainingTheCertificate { get; set; }
        [Required]
        public string YearsExperience { get; set; }
        [Required]
        public string TheUniversityFromWhichHeObtainedHisLastDegree { get; set; }
        [Required]
        public string TheCountryYouGraduatedFrom { get; set; }

        public ICollection<Doctor_Course> doctor_Courses { get; set; }
        public ICollection<Marks> Marks { get; set; }
        public ICollection<WeeklyEvaluation> WeeklyEvaluation { get; set; }
    }
}
