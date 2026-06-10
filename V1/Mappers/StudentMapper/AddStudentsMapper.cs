using DevetionStudetns.DTO.StudentsDTO;
using testDtoAndmapper.Entity;

namespace DevetionStudetns.Mappers.StudentsMapper
{
    public static class AddStudentsMapper
    {
        public static Student AddStudntsMapper (this StudentsAddDto students)
        {
            return new Student
            {
                UserId = students.UserId,
                CumulativeAverage = students.CumulativeAverage,              
                StudentLevel = students.StudentLevel,
                YearEnrollment = Convert.ToString(students.YearEnrollment),
            };
        }
    }
}
