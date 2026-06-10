using FinalProject.DTO.StudentsDTO;
using System.ComponentModel.DataAnnotations;
using testDtoAndmapper.Entity;
using V1.DTO.StudentsDTO;

namespace V1.Mappers.StudentMapper
{
    public static class UpdateStudentAndUserMapper
    {
        public static Student UpdateStudntsMapper(this UpdateStudentAndUserDto students, UpdateStudentAndUserDto users)
        {
            return new Student
            {
                CumulativeAverage = students.CumulativeAverage,
                StudentLevel = students.StudentLevel,
                YearEnrollment = students.YearEnrollment,

            };
        }
        public static Users UpdateStudntsMapper(this UpdateStudentAndUserDto users)
        {
            return new Users
            {
                UserId = users.UserId,

                IdNumber = users.IdNumber,

                FirstName = users.FirstName,

                LastName = users.LastName,

                FullName = users.FullName,

                Email = users.Email,

                Gender = users.Gender,

                Address = users.Address,

                PhoneNumber = users.PhoneNumber,

                DateOfBarth = users.DateOfBarth,

                RoleId = users.RoleId,

                AccountStatus = users.AccountStatus,

                Password = users.Password,
            };
        }
    }
}
