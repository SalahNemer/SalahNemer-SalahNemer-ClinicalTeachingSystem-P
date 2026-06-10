using DevetionStudetns.DTO.MainGroupDTO;
using DevetionStudetns.NewFolder;

namespace DevetionStudetns.Mappers.MainGroupMapoer
{
    public static class ShowMainGroup
    {
        public static GetMainGroupDto ShowMainGourp (this MainGrop mainGrop)
        {
            return new GetMainGroupDto
            {
                MainGroupId = mainGrop.MainGroupId,
                MainGroupSympole = mainGrop.MainGroupSympole,
                AcademicYearName = mainGrop.AcademicYearName,
                AcademicYearId = mainGrop.AcademicYearId,
            };
        }
    }
}
