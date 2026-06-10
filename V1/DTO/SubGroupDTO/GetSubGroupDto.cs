using DevetionStudetns.DTO.MainGroupDTO;

namespace DevetionStudetns.DTO.SubGroupDTO
{
    public class GetSubGroupDto 
    {
        public int SubGroupId { get; set; }
        public string SubGroupSympole { get; set; }
        public int MainGroupId { get; set; }
        public int NumberOfStudetns { get; set; }
    }
}
