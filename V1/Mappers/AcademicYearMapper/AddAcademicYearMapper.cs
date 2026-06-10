using DevetionStudetns.DTO.AcademicYearDTO;
using DevetionStudetns.NewFolder;

namespace DevetionStudetns.Mappers.AcademicYearMapper
{
    public static class AddAcademicYearMapper
    {
        public static AcademicYear AddAcademicYearMap(this GetAcademicYearDto academicYear)
        {
            return new AcademicYear
            {
                AcademicYearLevel = academicYear.AcademicYearLevel,
            };
        }
    }
}
