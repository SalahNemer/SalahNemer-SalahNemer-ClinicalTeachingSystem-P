using BuildDB_Team.entitys;
using database.models;
using DataBase.entitys;
using DevetionStudetns.entitys;
using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testDtoAndmapper.Entity;

namespace DataBase.Entity
{
    [Table("WeeklyEvaluation")]
    public class WeeklyEvaluation
    {
        [Key]
        [Column("WeeklyEvaluationId")]
        public int WeeklyEvaluationId { get; set; }
        [Required]
        [Column("AnswerTheQuestion")]
        public float AnswerTheQuestion {  get; set; }

        [Required]
        [Column("EntryDate")]
        public DateTime EntryDate { get; set; } = DateTime.Now;

        [Required]
        [Column("StudentId")]
        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Students { get; set; }
        [Required]
        [Column("CourseId")]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course course { get; set; } 

        
        [Required]
        [Column("DoctorId")]
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctors { get; set; } 

        [ForeignKey("AppointmentId")]
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }  

        [ForeignKey("EvaluationQuestionId")]
        public int EvaluationQuestionId { get; set; }
        public EvaluationQuestion EvaluationQuestion  { get; set; } 
        [ForeignKey("EvaluationFormId")]
        public int EvaluationFormId { get; set; }
        public EvaluationForm EvaluationForm { get; set; } 
    }
}
