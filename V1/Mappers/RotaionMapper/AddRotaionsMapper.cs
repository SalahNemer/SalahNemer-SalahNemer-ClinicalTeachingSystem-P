using DataBase.entitys;
using DevetionStudetns.DTO.RotationsDTO;

namespace DevetionStudetns.Mappers.RotaionsMappers
{
    public static class AddRotaionsMapper
    {
        public static Rotation AddRotations (this AddRotaionsDto addRotaionsDto)
        {
            return new Rotation
            {
                StartRotationDate = addRotaionsDto.StartRotationDate,
                EndRotationDate = addRotaionsDto.EndRotationDate,
                AcademicYearName = addRotaionsDto.AcademicYearName,
                RotationName = addRotaionsDto.RotationName,
            };
        }
    }
}
