using DevetionStudetns.DTO.DistributionsMainGroupDTO;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using V1.DTO.DistributionMainGroupDTO;

namespace DevetionStudetns.Service
{
    public class DistributionsMainGroupService
    {
        private readonly IDistributionsMainGroup _context;
        public DistributionsMainGroupService(IDistributionsMainGroup context)
        {
            _context = context;
        }
        public GeneralMsgDto AddDistributionsMainGroupService(AddDistributionsMainGroupDto addDistributionsMainGroupDto)
        {
            return _context.AddDistributionsMainGroup(addDistributionsMainGroupDto);
        }
        public GeneralMsgDto DeleteDistributionsMainGroupService(int DistributionsMainGroupId)
        {
            return _context.DeleteDistributionsMainGroup(DistributionsMainGroupId);
        }
        public GeneralMsgDto UpdateDistributionsMainGroupService(UpdateDistributionsMainGroupDto NewDistributionsMainGroupDto, int DistributionsMainGroupId)
        {
            return _context.UpdateDistributionsMainGroup(NewDistributionsMainGroupDto, DistributionsMainGroupId);
        }
        public List<ShowDistrbutionMainQDto> ShowDistributionFroTheMainGroupService(int MainGroupId)
        {
            return _context.ShowDistributionFroTheMainGroup(MainGroupId);
        }
        public List<ShowDistrbutionMainQDto> ShowDistributionMainGroupFroTheLevelService(int Level)
        {
            return _context.ShowDistributionMainGroupFroTheLevel(Level);
        }
        public List<GetDistrbutionMianGroupQ1Dto> ShowDistributionMainGroupByLevelAndRotationIdService(int Level, string AcademicYearName)
        {
            return _context.ShowDistributionMainGroupByLevelAndRotationId(Level, AcademicYearName);
        }
        public List<ShowDistrbutionMainQDto> ShowDistributionFroTheMainGroupByDistributionsMainGroupIdService(int DistributionsMainGroupId)
        {
            return _context.ShowDistributionFroTheMainGroupByDistributionsMainGroupId(DistributionsMainGroupId);
        }

    }
}
