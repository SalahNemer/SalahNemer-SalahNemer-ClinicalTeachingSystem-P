using DevetionStudetns.DTO.MainGroupDTO;
using DevetionStudetns.NewFolder;
using loginpage.DBcon;

namespace FinalProject.Interface.IRepositry
{
    public interface IMainGroup
    {
        public GeneralMsgDto AddMianGroupRepo(AddMainGroupDto mainGroupDto);
        public List<GetMainGroupDto> GetMianGroupByYearAndACYRepo(string acadimicYear, int level);
        public GetMainGroupDto GetMianGroupBySemolyRepo(int getMainGroupById);
        public GeneralMsgDto DeleteMainGroup(int mainGroupId);
        public GeneralMsgDto UpdateMainGroupRepo(AddMainGroupDto NewDataMainGoup, int MainGroupId);
        public string getMainGroupSympole(int mainGroupid);
        public List<GetMainGroupDto> GetMianGroupByYearAndACYRepoForTheDistpution(string acadimicYear, int level);
    }
}
