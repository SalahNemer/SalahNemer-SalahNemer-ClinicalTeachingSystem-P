using DevetionStudetns.DTO.DistributionsMainGroupDTO;
using DevetionStudetns.Entity;

namespace DevetionStudetns.Mappers.AddDistributionsMainGroupMapper
{
    public static class AddDistributionsMainGroupMapper
    {
        public static DistributionsMainGroup AddDistributionsMainGroup (this AddDistributionsMainGroupDto addDistributionsMainGroupDto)
        {
            return new DistributionsMainGroup
            {
                DepartmentId = addDistributionsMainGroupDto.DepartmentId,
                MainGroupId = addDistributionsMainGroupDto.MainGroupId,
                RotationId = addDistributionsMainGroupDto.RotationId
            };
        }

    }
}
