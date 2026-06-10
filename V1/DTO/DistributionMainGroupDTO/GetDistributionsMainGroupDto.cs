using DataBase.entitys;
using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DevetionStudetns.DTO.DistributionsMainGroupDTO
{
    public class GetDistributionsMainGroupDto
    {
        public int DistributionsMainGroupId { get; set; }
        public int MainGroupId { get; set; }
        public int RotationId { get; set; }
        public int DepartmentId { get; set; }
    }
}
