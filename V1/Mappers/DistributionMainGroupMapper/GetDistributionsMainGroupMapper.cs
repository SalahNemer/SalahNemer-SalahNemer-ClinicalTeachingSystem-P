using DevetionStudetns.DTO.DistributionsMainGroupDTO;
using DevetionStudetns.Entity;

namespace DevetionStudetns.Mappers.DistributionsMainGroupMapper
{
    public static class GetDistributionsMainGroupMapper
    {
        public static GetDistributionsMainGroupDto ShowDistributionsMainGroupMap ( this DistributionsMainGroup distributionsMainGroup )
        {
            return new GetDistributionsMainGroupDto
            {
                DistributionsMainGroupId = distributionsMainGroup.DistributionsMainGroupId,
                DepartmentId = distributionsMainGroup.DepartmentId,
                MainGroupId = distributionsMainGroup.MainGroupId,
                RotationId = distributionsMainGroup.RotationId
            };
        }
    }
}
