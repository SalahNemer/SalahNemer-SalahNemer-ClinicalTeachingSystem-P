using DataBase.entitys;
using DevetionStudetns.DTO.DistributionsDTO;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using V1.DTO.DistributionDTO;

namespace DevetionStudetns.Service
{
    public class DistriputionService
    {
        private readonly IDistribution _context;
        public DistriputionService(IDistribution context)
        {
            _context = context;
        }

        public GeneralMsgDto AddDistributionService(AddDistibutionsDto addDistibutionsDto)
        {
            return _context.AddDistribution(addDistibutionsDto);
        }
        public GeneralMsgDto DeleteDistubutionService(int DistrbutionId)
        {
            return _context.DeleteDistubution(DistrbutionId);
        }
        public GeneralMsgDto UpdateDistrbutionService(AddDistibutionsDto NewDistrbutionData, int DistrbutionId)
        {
            return _context.UpdateDistrbution(NewDistrbutionData, DistrbutionId);
        }
        public List<GetDistbutionsQDto> getAllDistbutionByMainGroupIdService(int mainGroupId, int RotationId)
        {
            return _context.getAllDistbutionByMainGroupId(mainGroupId, RotationId);

        }

        public List<GetDistbutionsQDto> getAllDistbutionBySubGroupIdService(int SubGroupId, int RotationId)
        {
            return _context.getAllDistbutionBySubGroupId(SubGroupId, RotationId);

        }

        public List<GetDistbutionsQ1Dto> getAllDistbutionToTheDoctorByDoctorIdAndRotationIdService(string DoctorId)
        {
            return _context.getAllDistbutionToTheDoctorByDoctorIdAndRotationId(DoctorId);
        }
        public List<GetDistbutionsQDto> getDistbutionForTheStudentsByStudentsIdService(string StudentsId)
        {
            return _context.getDistbutionForTheStudentsByStudentsId(StudentsId);
        }
        public List<GetDistbutionsQDto> getAllDistbutionIdService(int DistributionId)
        {
            return _context.getAllDistbutionId(DistributionId);
        }
        public List<GetDocotorCourseDataQDto> GetDoctorByRotationIdMainGroupAndAcadmicYear(int MainGroup, int RotationId, string AcadmicYear)
        {
            return _context.GetDoctorByRotationIdMainGroupAndAcadmicYear(MainGroup,RotationId,AcadmicYear);

        }
        public List<GetDistbutionsQ2Dto> GetCourseByDctorIdAndAcadmicYear(string DoctorId, string AcadmicYear)
        {
            return _context.GetCourseByDctorIdAndAcadmicYear(DoctorId, AcadmicYear);
        }


    }
}
