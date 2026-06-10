using V1.DTO.PolicieDTO;
using V1.Entity;

namespace V1.Mappers.PolicieMapper
{
    public static class AddPolicieMapper
    {
        public static Policie AddPolicie (this AddPolicieDto addPolicieDto)
        {
            return new Policie
            {
                CreatorId = addPolicieDto.CreatorId,
                Forms = addPolicieDto.Forms,
                Objectives = addPolicieDto.Objectives,
                ExecutionResponsible = addPolicieDto.ExecutionResponsible,
                PolicyIdentifier = addPolicieDto.PolicyIdentifier,
                Procedures = addPolicieDto.Procedures,
                Title = addPolicieDto.Title,
            };
        }
    }
}
