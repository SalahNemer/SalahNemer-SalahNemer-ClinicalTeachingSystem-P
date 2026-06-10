using DevetionStudetns.DTO.SubGroupDTO;
using loginpage.DBcon;

namespace FinalProject.Interface.IRepositry
{
    public interface ISubGroup
    {
        public GeneralMsgDto AddSubGroup(AddSubGroupDto supGroupDto);
        public GeneralMsgDto DeleteSubGroup(int subGrooupId);
        public List<GetSubGroupDto> GetSubGroup(int MainGroupId);
        public List<GetSubGroupDto> GetAllSuBGroupByMainGroupIdRepo(int MainGroupId);
        public GetSubGroupDto GetSubGroupById(int SubGroupId);
        public GeneralMsgDto UpdateSubGroupById(AddSubGroupDto NewDataSubGoup, int subGroupId);
    }
}
