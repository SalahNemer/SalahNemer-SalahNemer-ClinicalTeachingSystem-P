using DevetionStudetns.DTO.AcademicYearDTO;
using DevetionStudetns.NewFolder;

namespace DevetionStudetns.Mappers.AcademicYearMapper
{
    public static class GetAcademicYearMapper
    {
        public static GetAcademicYearDto ShwoAcademicYearMapper(this AcademicYear academicYear)
        {
            return new GetAcademicYearDto
            {
                AcademicYearLevel = academicYear.AcademicYearLevel,
            };
        }
    }
}
