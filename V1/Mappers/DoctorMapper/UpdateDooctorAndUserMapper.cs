using database.models;
using DevetionStudetns.DTO.DoctorDto;
using testDtoAndmapper.Entity;
using V1.DTO.DoctorDTO;
using V1.DTO.StudentsDTO;

namespace V1.Mappers.DoctorMapper
{
    public static class UpdateDooctorAndUserMapper
    {
        public static Doctor Doctor(this UpdateDoctorAndUserDto dto)
        {
            return new Doctor
            {
                UserId = dto.DoctorId,
                HospitalId = dto.HospitalId,
                DepartmentID = dto.DepartmentID,
                MedicalSpecialty = dto.MedicalSpecialty,
                AcademicDegree = dto.AcademicDegree,
                YearOfObtainingTheCertificate = dto.YearOfObtainingTheCertificate,
                YearsExperience = dto.YearsExperience,
                TheUniversityFromWhichHeObtainedHisLastDegree = dto.TheUniversityFromWhichHeObtainedHisLastDegree,
                TheCountryYouGraduatedFrom = dto.TheCountryYouGraduatedFrom,
            };
        }
        public static Users UpdateDooctorAndUserMapp(this UpdateDoctorAndUserDto users)
        {
            return new Users
            {
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
