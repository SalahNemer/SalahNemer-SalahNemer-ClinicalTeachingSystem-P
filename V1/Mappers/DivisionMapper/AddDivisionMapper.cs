using DataBase.entitys;
using DevetionStudetns.DTO.DivisionsDTO;

namespace DevetionStudetns.Mappers.DivisionMapper
{
    public static class AddDivisionMapper
    {
        public static Division AddDivisionMappers (this DivisionAddDto divisionAddDto)
        {
            return new Division
            {
                StudentId = divisionAddDto.StudentId,
                SubGroupId = divisionAddDto.SubGroupId,
                DivisionStatus = 1
            };
        }
        
    }
}
