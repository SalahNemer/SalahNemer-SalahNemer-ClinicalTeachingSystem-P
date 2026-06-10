using DevetionStudetns.DTO.UserDTO;

namespace DevetionStudetns.DTO.StudentsDTO
{
    public class GetStudntsDto 
    {
        public string FullName { get; set; }
        public string UserId { get; set; }
        public string YearEnrollment { get; set; }
        public double CumulativeAverage { get; set; }
        public int StudentLevel { get; set; }
    }
}
