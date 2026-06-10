using database.models;
using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace V1.DTO.DoctorCrouseDTO
{
    public class AddDoctorCourseDto
    {
        public int CourseId { get; set; }
        public string DoctorId { get; set; }
        public string CurrentAcademicYearName { get; set; }
    }
}
