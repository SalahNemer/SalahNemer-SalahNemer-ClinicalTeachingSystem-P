using DevetionStudetns.DTO.DistributionsMainGroupDTO;
using loginpage.DBcon;
using V1.DTO.DistributionMainGroupDTO;

namespace FinalProject.Interface.IRepositry
{
    public interface IDistributionsMainGroup
    {
        public GeneralMsgDto AddDistributionsMainGroup(AddDistributionsMainGroupDto addDistributionsMainGroupDto);
        public GeneralMsgDto DeleteDistributionsMainGroup(int DistributionsMainGroupId);
        public GeneralMsgDto UpdateDistributionsMainGroup(UpdateDistributionsMainGroupDto NewDistributionsMainGroupDto, int DistributionsMainGroupId);
        public List<ShowDistrbutionMainQDto> ShowDistributionFroTheMainGroup(int MainGroupId);
        public List<ShowDistrbutionMainQDto> ShowDistributionMainGroupFroTheLevel(int Level);
        public List<GetDistrbutionMianGroupQ1Dto> ShowDistributionMainGroupByLevelAndRotationId(int Level, string AcademicYearName);
        public List<ShowDistrbutionMainQDto> ShowDistributionFroTheMainGroupByDistributionsMainGroupId(int DistributionsMainGroupId);
    }
}
