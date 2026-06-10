using database.models;
using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using testDtoAndmapper.Entity;
using DataBase.entitys;
using DevetionStudetns.entitys;

namespace V1.DTO.WeeklyEvaluationDTO
{
    public class GetWeeklyEvaluationDto
    {
        public int WeeklyEvaluationId { get; set; }
        public int AppointmentId { get; set; }
        public int CourseId { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string DoctorId { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public int EvaluationFormId { get; set; }
        public int EvaluationQuestionId { get; set; }
        public float AnswerTheQuestion { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now;
        public string FormattedDateTime => EntryDate.ToString("yyyy-MM-dd / HH:mm:ss");
    }
}
