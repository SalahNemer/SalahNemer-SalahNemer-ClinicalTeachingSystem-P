using DevetionStudetns.DTO.MainGroupDTO;
using DevetionStudetns.NewFolder;

namespace DevetionStudetns.Mappers.MainGroupMapoer
{
    public static class AddMainGroupMapper
    {
        public static MainGrop AddMainGroupMap(this AddMainGroupDto mainGroupDto)
        {
            return new MainGrop
            {
                MainGroupSympole = mainGroupDto.MainGroupSympole,
                AcademicYearId = mainGroupDto.AcademicYearId,
                AcademicYearName = mainGroupDto.AcademicYearName,
            };
        }
    }
}
