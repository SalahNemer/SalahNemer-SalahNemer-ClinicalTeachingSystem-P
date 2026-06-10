namespace V1.DTO.DoctorCrouseDTO
{
    public class GetSuperviseDoctorQDto
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public int HospitalId { get; set; }
        public int DepartmentId { get; set; }
        public string MedicalSpecialty { get; set; }
        public string YearOfObtainingTheCertificate { get; set; }
        public string AcademicDegree { get; set; }
        public string TheCountryYouGraduatedFrom { get; set; }
        public string TheUniversityFromWhichHeObtainedHisLastDegree { get; set; }
        public string YearsExperience { get; set; }
    }
}
