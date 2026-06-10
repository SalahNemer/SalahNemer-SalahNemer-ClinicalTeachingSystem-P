using DevetionStudetns.DTO.MainGroupDTO;
using DevetionStudetns.NewFolder;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;

namespace DevetionStudetns.Service
{
    public class MainGroupService
    {
        private readonly IMainGroup _context;
        public MainGroupService(IMainGroup mianGroupServeice)
        {
            _context = mianGroupServeice;
        }
        
        public GeneralMsgDto AddMianGroupServiece(AddMainGroupDto mainGroupDto)
        {
            return _context.AddMianGroupRepo(mainGroupDto);
        }
        public List<GetMainGroupDto> GetMianGroupByYearAndACYRepo(string acadimicYear, int level)
        {
            return _context.GetMianGroupByYearAndACYRepo(acadimicYear, level);
        }

        public GetMainGroupDto GetMianGroupBySemolyService(int getMainGroupById)
        {
            return _context.GetMianGroupBySemolyRepo(getMainGroupById);
        }

        public GeneralMsgDto DeleteMainGroupInService (int mainGroupId)
        {
            return _context.DeleteMainGroup(mainGroupId);
        }
        public GeneralMsgDto UpdateMainGroupService(AddMainGroupDto NewDataMainGoup, int MianGroupId)
        {
            return _context.UpdateMainGroupRepo(NewDataMainGoup, MianGroupId);
        }
        public List<GetMainGroupDto> GetMianGroupByYearAndACYRepoForTheDistputionSerive(string acadimicYear, int level)
        {
            return _context.GetMianGroupByYearAndACYRepoForTheDistpution(acadimicYear, level);
        }

    }
}
