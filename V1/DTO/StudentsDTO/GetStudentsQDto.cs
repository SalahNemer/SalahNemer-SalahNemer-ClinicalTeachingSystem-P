namespace DevetionStudetns.DTO.StudentsDTO
{
    public class GetStudentsQDto
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string IdNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int StudentLevel { get; set; }
        public double CumulativeAverage { get; set; }     
        public string YearEnrollment { get; set; }  
        public string Address { get; set; } 

    }
}
