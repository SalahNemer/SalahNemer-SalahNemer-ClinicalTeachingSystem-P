using loginpage.DBcon;
using V1.DTO.PolicieDTO;

namespace V1.Interface.IRepositry
{
    public interface IPolicie
    {
        public GeneralMsgDto AddPolicie(AddPolicieDto AddPolicie);
        public GeneralMsgDto UpdatePolicie(UpdatePolicieDto newDataPolicieDto, int PolicieId);
        public GeneralMsgDto DeletePolicie(int policieId);
        public List<GetPolicieQDto> GetAllPolicie();
        public List<GetPolicieQDto> GetAllPolicieById(int PolicieId);
    }
}
