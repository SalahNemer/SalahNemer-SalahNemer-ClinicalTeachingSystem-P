using DevetionStudetns.DTO.SubGroupDTO;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;

namespace DevetionStudetns.Service
{
    public class SubGroupService
    {
        private readonly ISubGroup _context;
        public SubGroupService(ISubGroup context)
        {
            _context = context;
        }

        public GeneralMsgDto AddSubGroupService(AddSubGroupDto supGroupDto)
        {
            return _context.AddSubGroup(supGroupDto);
        }
        public GeneralMsgDto DeleteSubGroupService(int subGrooupId)
        {
            return _context.DeleteSubGroup(subGrooupId);
        }
        public List<GetSubGroupDto> GetSubGroupService(int MainGroupId)
        {
            return _context.GetSubGroup(MainGroupId);
        }
        public List<GetSubGroupDto> GetAllSuBGroupByMainGroupIdService(int MainGroupId)
        {
            return _context.GetAllSuBGroupByMainGroupIdRepo(MainGroupId);
        }
        public GetSubGroupDto GetSubGroupByIdService(int SubGroupId)
        {
            return _context.GetSubGroupById(SubGroupId);
        }
        public GeneralMsgDto UpdateSubGroupByIdService(AddSubGroupDto NewDataSubGoup, int subGroupId)
        {
            return _context.UpdateSubGroupById(NewDataSubGoup, subGroupId);
        }



    }
}
