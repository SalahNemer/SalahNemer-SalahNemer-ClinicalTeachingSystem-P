using database.models;
using DevetionStudetns.DTO.DoctorDto;
using FinalProject.DTO.DoctorDto;

namespace DevetionStudetns.Mappers.DoctorMappier
{
    public static class DoctorMapper
    {
        public static Doctor Doctor(this DoctorDto dto)
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

        public static DoctorDto GetDoc(this Doctor dto)
        {
            return new DoctorDto
            {
                DoctorId = dto.UserId,
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
        public static Doctor UpdateDoctor(this UpdateDoctorDto dto)
        {
            return new Doctor
            {
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

    }
}
