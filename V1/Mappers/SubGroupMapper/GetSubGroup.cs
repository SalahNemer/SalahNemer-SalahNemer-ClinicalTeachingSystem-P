using DevetionStudetns.DTO.MainGroupDTO;
using DevetionStudetns.DTO.SubGroupDTO;
using DevetionStudetns.NewFolder;

namespace DevetionStudetns.Mappers.SubGroupMapper
{
    public static class GetSubGroup
    {
        public static GetSubGroupDto showSubGroupMapper (this SubGroup subGroup)
        {
            return new GetSubGroupDto
            {
                SubGroupId = subGroup.SubGroupId,
                SubGroupSympole = subGroup.SubGroupSympole,
                MainGroupId = subGroup.MainGroupId,
                NumberOfStudetns = (int)subGroup.NumberOfStudetns,
            };
        }
    }
}
