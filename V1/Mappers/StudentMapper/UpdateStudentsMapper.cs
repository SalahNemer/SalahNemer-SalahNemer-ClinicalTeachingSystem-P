using DevetionStudetns.DTO.StudentsDTO;
using FinalProject.DTO.StudentsDTO;
using testDtoAndmapper.Entity;

namespace FinalProject.Mappers.StudentsMapper
{
    public static class UpdateStudentsMapper
    {
        public static Student AddStudntsMapper(this UpdateStudentsDto students)
        {
            return new Student
            {
                CumulativeAverage = students.CumulativeAverage,            
                StudentLevel = students.StudentLevel,
                YearEnrollment = students.YearEnrollment,

            };
        }
    }
}
