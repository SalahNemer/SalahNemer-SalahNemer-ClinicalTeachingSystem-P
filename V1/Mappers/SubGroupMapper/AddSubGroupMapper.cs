using DevetionStudetns.DTO.SubGroupDTO;
using DevetionStudetns.NewFolder;

namespace DevetionStudetns.Mappers.SubGroupMapper
{
    public static class AddSubGroupMapper
    {
        public static SubGroup AddSupGroupMapper(this AddSubGroupDto supGroupDto)
        {
            return new SubGroup
            {
                SubGroupSympole = supGroupDto.SubGroupSympole,
                MainGroupId = supGroupDto.MainGroupId,
                NumberOfStudetns = 0
            };
        }

    }
}
