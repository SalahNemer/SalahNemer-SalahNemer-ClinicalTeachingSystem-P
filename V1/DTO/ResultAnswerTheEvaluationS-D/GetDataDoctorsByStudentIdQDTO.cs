namespace V1.DTO.StudentsDTO
{
    public class GetDataDoctorsByStudentIdQDTO
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public int? Appointmentid { get; set; }
        public DateOnly StartSessionDate { get; set; }
        public DateOnly EndSessionDate { get; set; }
    }
}
