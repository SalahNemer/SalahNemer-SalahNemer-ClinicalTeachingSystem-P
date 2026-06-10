using DevetionStudetns.DTO.DistributionsDTO;
using loginpage.DBcon;
using V1.DTO.DistributionDTO;

namespace FinalProject.Interface.IRepositry
{
    public interface IDistribution
    {
        public GeneralMsgDto AddDistribution(AddDistibutionsDto addDistibutionsDto);
        public GeneralMsgDto DeleteDistubution(int DistrbutionId);
        public GeneralMsgDto UpdateDistrbution(AddDistibutionsDto NewDistrbutionData, int DistrbutionId);
        public List<GetDistbutionsQDto> getAllDistbutionByMainGroupId(int mainGroupId, int RotationId);
        public List<GetDistbutionsQDto> getAllDistbutionBySubGroupId(int SubGroupId, int RotationId);
        public List<GetDistbutionsQ1Dto> getAllDistbutionToTheDoctorByDoctorIdAndRotationId(string DoctorId);
        public List<GetDistbutionsQDto> getDistbutionForTheStudentsByStudentsId(string StudentsId);
        public List<GetDistbutionsQDto> getAllDistbutionId(int DistributionId);
        public List<GetDocotorCourseDataQDto> GetDoctorByRotationIdMainGroupAndAcadmicYear(int MainGroup, int RotationId, string AcadmicYear);
        public List<GetDistbutionsQ2Dto> GetCourseByDctorIdAndAcadmicYear(string DoctorId, string AcadmicYear);
    }
}
