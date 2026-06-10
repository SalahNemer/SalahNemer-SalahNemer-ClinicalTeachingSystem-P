using DevetionStudetns.DTO.RotationsDTO;
using loginpage.DBcon;

namespace FinalProject.Interface.IRepositry
{
    public interface IRotations
    {
        public GeneralMsgDto AddRotations(AddRotaionsDto addRotaionsDto);
        public GeneralMsgDto DeleteRotations(int RotationsId);
        public GeneralMsgDto UpdateRotations(AddRotaionsDto NewRotaionsData, int RotaionstId);
        public List<GetRotaionDto> ShowDateRotaionInOneYear(string Year);
        public GetRotaionDto ShowDateRotaionByRotationId(int RotationId);
    }
}
