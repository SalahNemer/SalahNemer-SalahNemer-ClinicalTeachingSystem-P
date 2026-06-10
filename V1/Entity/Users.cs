using BuildDB_Team.entitys;
using database.models;
using DataBase.Entity;
using DataBase.entitys;
using DevetionStudetns.entitys;
using DevetionStudetns.NewFolder;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UploadReportsCode.Entity;
using V1.Entity;

namespace testDtoAndmapper.Entity
{
    [Table("Users")]
    public class Users
    {
        [Key]
        [Column("UserId")]
        [MaxLength(20)]
        public string UserId { get; set; }
        [Required]
        [MaxLength(20)]
        [Column("IdNumber")]
        public string IdNumber { get; set; }
        [Required]
        [MaxLength(100)]
        [Column("FirstName")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        [Column("LastName")]
        public string LastName { get; set; }
        [Required]
        [Column("FullName")]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [MaxLength(254)]
        [Column("Email")]
        public string Email { get; set; }
        [Required]
        [MaxLength(6)]
        [Column("Gender")]
        public string Gender { get; set; }
        [Required]
        [Column("Address")]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(20)]
        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Required]
        [Column("DateOfBarth")]
        public DateOnly DateOfBarth { get; set; }
        [Column("RoulName")]
        public int? RoleId { get; set; }
        [Column("AccountStatus")]
        public byte? AccountStatus { get; set; } 
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
        public ICollection<Distribution> Distributions { get; set; }
        public ICollection<Division>  divisions { get; set; }
        public Student students { get; set; }
        public Doctor doctors { get; set; }
        public TA TA { get; set; }
        public ICollection<Message> UserSender { get; set; }
        public ICollection<Message> UserResever { get; set; }
        public ICollection<Questionnaire> Questionnaire { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<AnswerTheEvaluation> EvaluatedPersonUser { get; set; }
        public ICollection<AnswerTheEvaluation> EvaluatorPersonUser { get; set; }
        public ICollection<Attendance> DoctorAttendances { get; set; }
        public ICollection<Attendance> StudentAttendances { get; set; }
        public ICollection<Policie> CreatePolicie { get; set; }
    }
}
