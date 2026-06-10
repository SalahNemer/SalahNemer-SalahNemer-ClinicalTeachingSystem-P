using loginpage.DBcon;
using V1.DTO.ApprovalsDTO;
using V1.DTO.MarkDTO;

namespace V1.Interface.IRepositry
{
    public interface IApprovals
    {
        public GeneralMsgDto ApprovalsForTheDivision(string CurrentAcadmicYear);
        public List<GetApprovalsQDto> GetListOfDivisionForTheApprovles(string AcademicYearName);
        public GeneralMsgDto UpdateApprovalsForTheDivision(UpdateApprovalsDto updateApprovalsDto, string AcademicYearName);
        public List<GetApprovalsQ1Dto> DivisionsNotApprovedByTheDean(string AcademicYearName);
        public List<GetApprovalsQDto> DivisionsApprovedByTheDean(string AcademicYearName);
        public GeneralMsgDto ApprovalsForTheDistbution(string AcademicYearName);
        public List<GetApprovalsQ2Dto> GetListOfDistributionForTheApprovles(string AcademicYearName);
        public GeneralMsgDto UpdateApprovalsForTheDistribution(UpdateApprovals1Dto updateApprovalsDto, string AcademicYearName);
        public List<GetApprovalsQ3Dto> DistributionNotApprovedByTheDean(string AcademicYearName);
        public List<GetApprovalsQ2Dto> DistributionApprovedByTheDean(string AcademicYearName);
        public GeneralMsgDto ReSendApprovalsForTheDistbution(string AcademicYearName);
        public GeneralMsgDto ReSendApprovalsForTheDivision(string AcademicYearName);
        public GeneralMsgDto SendMarkToTheDepartmetnsHead(string DoctorId);
        public GeneralMsgDto ReSendMarkToTheDepartmetnsHead(string DoctorId);
        public GeneralMsgDto UpdateApprovalsForTheMark(UpdateApprovalsDto updateApprovalsDto, int markId);
        public List<GetApprovlesQ4Dto> GetListOfMarkForTheApprovlesForTheDepartmentHead(string DepartmentHeadId);
        public List<GetApprovlesQ4Dto> MarkNotApprovedByTheDeprtmentHead();
        public List<GetApprovlesQ4Dto> MarkApprovedByTheDeprtmentHead();
        public List<GetApprovlesQ4Dto> GetListOfMarkForTheApprovlesForTheClinicalDepartmentDirector();
        public List<GetApprovlesQ4Dto> MarkNotApprovedByTheClinicalDepartmentDirector();
        public List<GetApprovlesQ4Dto> MarkApprovedByTheClinicalDepartmentDirector();
        public List<GetApprovlesQ4Dto> GetListOfMarkForTheApprovlesForTheDean();
        public List<GetApprovlesQ4Dto> MarkNotApprovedByTheDean();
        public List<GetApprovlesQ4Dto> MarkApprovedByTheDean();
        public List<GetApprovalsQ2Dto> GetListOfDistributionAfterTheApprovles(string AcademicYearName);
        public List<GetApprovalsQDto> GetListOfDivisionAfterTheApprovles(string AcademicYearName);
        public List<GetMarkQ5Dto> GetMarkApprovedToTheDoctor(string doctorId);
        public List<GetMarkQ5Dto> GetMarkNotApprovedToTheDoctor(string doctorId);
    }
}
