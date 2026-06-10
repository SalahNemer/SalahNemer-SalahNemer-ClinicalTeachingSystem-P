using DataBase.entitys;
using DevetionStudetns.DTO.RotationsDTO;

namespace DevetionStudetns.Mappers.RotaionsMappers
{
    public static class GetRotationsMapper
    {
        public static GetRotaionDto ShowRotations(this Rotation addRotaionsDto)
        {
            return new GetRotaionDto
            {
                RotationId = addRotaionsDto.RotationId,
                StartRotationDate = addRotaionsDto.StartRotationDate,
                EndRotationDate = addRotaionsDto.EndRotationDate,
                RotationName = addRotaionsDto.RotationName,
                AcademicYearName = addRotaionsDto.AcademicYearName,
            };
        }
    }
}
