using DataBase.entitys;
using DevetionStudetns.DTO.DivisionsDTO;

namespace DevetionStudetns.Mappers.DivisionMapper
{
    public static class GetDivisionMapper
    {
        public static GetDivisionDto showDivisionMap (this Division divisions)
        {
            return new GetDivisionDto
            {
                StudentId = divisions.StudentId,    
                SubGroupId = divisions.SubGroupId,
                DivisionStatus = (int)divisions.DivisionStatus,
            };
        }
    }
}
