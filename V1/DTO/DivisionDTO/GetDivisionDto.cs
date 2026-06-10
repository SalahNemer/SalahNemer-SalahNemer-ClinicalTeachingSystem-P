using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DevetionStudetns.DTO.DivisionsDTO
{
    public class GetDivisionDto
    {
        public int SubGroupId { get; set; }
        public string StudentId { get; set; }
        public int DivisionStatus { get; set; } 

    }
}
