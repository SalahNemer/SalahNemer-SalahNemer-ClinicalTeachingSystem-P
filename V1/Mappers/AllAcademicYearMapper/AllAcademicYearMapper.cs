using V1.abed.AllAcademicYearDTO;
using V1.Entity;

namespace V1.abed.AllAcademicYearMapper
{
    public static class AllAcademicYearMapper
    {
        public static AllAcademinYears AddAcademicYearsMappers (this AddAllAcademicYearDto addAllAcademicYearDto)
        {
            return new AllAcademinYears
            {
                CurrentAcademicYearName = addAllAcademicYearDto.CurrentAcademicYearName,
            };
        }
    }
}
