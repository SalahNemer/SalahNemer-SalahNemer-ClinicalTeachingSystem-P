namespace V1.DTO.StudentsDTO
{
    public class GetStudentsQ2Dto
    {
        public int DivisionId { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string IdNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int StudentLevel { get; set; }
        public double CumulativeAverage { get; set; }  
    }
}
