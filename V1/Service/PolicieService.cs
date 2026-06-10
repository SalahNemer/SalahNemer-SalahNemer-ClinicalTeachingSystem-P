using loginpage.DBcon;
using V1.DTO.PolicieDTO;
using V1.Interface.IRepositry;

namespace V1.Service
{
    public class PolicieService
    {
        private readonly IPolicie _context; 
        public PolicieService(IPolicie con )
        {
            _context = con;
        }
        public GeneralMsgDto AddPolicieService(AddPolicieDto AddPolicie)
        {
            return _context.AddPolicie(AddPolicie); 
        }
        public GeneralMsgDto UpdatePolicieService(UpdatePolicieDto newDataPolicieDto, int PolicieId)
        {
            return _context.UpdatePolicie(newDataPolicieDto, PolicieId);
        }
        public GeneralMsgDto DeletePolicieService(int policieId)
        {
            return _context.DeletePolicie(policieId);
        }
        public List<GetPolicieQDto> GetAllPolicieService()
        {
            return _context.GetAllPolicie();
        }
        public List<GetPolicieQDto> GetAllPolicieByIdService(int PolicieId)
        {
            return _context.GetAllPolicieById(PolicieId);
        }

    }
}
