using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DevetionStudetns.DTO.MainGroupDTO
{
    public class GetMainGroupDto
    {
        public int MainGroupId { get; set; }
        public string MainGroupSympole { get; set; }
        public int AcademicYearId { get; set; }
        public string AcademicYearName { get; set; }
    }
}
