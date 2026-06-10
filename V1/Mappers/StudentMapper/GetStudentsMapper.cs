using DevetionStudetns.DTO.StudentsDTO;
using System.Runtime.CompilerServices;
using testDtoAndmapper.Entity;

namespace DevetionStudetns.Mappers.StudentsMapper
{
    public static class GetStudentsMapper
    {
        public static GetStudntsDto ShwoStudentsDataInMapper (this Student students )
        {
             return new GetStudntsDto
             {
                 UserId = students.UserId,
                 CumulativeAverage = students.CumulativeAverage,              
                 StudentLevel = students.StudentLevel,
                 YearEnrollment = students.YearEnrollment,
             };
           
        }
    }
}
