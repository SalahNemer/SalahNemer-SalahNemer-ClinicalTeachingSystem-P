using loginpage.DBcon;
using V1.DTO.ApprovalsDTO;
using V1.DTO.MarkDTO;
using V1.Interface.IRepositry;

namespace V1.Service
{
    public class ApprovalsService
    {
        private readonly IApprovals _context;
        public ApprovalsService(IApprovals context)
        {
            _context = context;
        }
        public GeneralMsgDto ApprovalsForTheDivisionService(string CurrentAcadmicYear)
        {
            return _context.ApprovalsForTheDivision(CurrentAcadmicYear);
        }
        public List<GetApprovalsQDto> GetListOfDivisionForTheApprovlesService(string AcademicYearName)
        {
            return _context.GetListOfDivisionForTheApprovles( AcademicYearName);
        }
        public GeneralMsgDto UpdateApprovalsForTheDivisionService(UpdateApprovalsDto updateApprovalsDto , string AcademicYearName)
        {
            return _context.UpdateApprovalsForTheDivision(updateApprovalsDto,  AcademicYearName);
        }
        public List<GetApprovalsQ1Dto> DivisionsNotApprovedByTheDeanService(string AcademicYearName)
        {
            return _context.DivisionsNotApprovedByTheDean( AcademicYearName);
        }
        public List<GetApprovalsQDto> DivisionsApprovedByTheDeanService(string AcademicYearName)
        {
            return _context.DivisionsApprovedByTheDean( AcademicYearName);
        }
        public GeneralMsgDto ApprovalsForTheDistbutionService(string AcademicYearName)
        {
            return _context.ApprovalsForTheDistbution(AcademicYearName  );
        }
        public List<GetApprovalsQ2Dto> GetListOfDistributionForTheApprovlesService(string AcademicYearName)
        {
            return _context.GetListOfDistributionForTheApprovles(AcademicYearName);
        }
        public GeneralMsgDto UpdateApprovalsForTheDistributionService(UpdateApprovals1Dto updateApprovalsDto, string AcademicYearName)
        {
            return _context.UpdateApprovalsForTheDistribution(updateApprovalsDto, AcademicYearName);
        }
        public List<GetApprovalsQ3Dto> DistributionNotApprovedByTheDeanService(string AcademicYearName)
        {
            return _context.DistributionNotApprovedByTheDean(AcademicYearName);
        }
        public List<GetApprovalsQ2Dto> DistributionApprovedByTheDeanService( string AcademicYearName)
        {
            return _context.DistributionApprovedByTheDean( AcademicYearName);
        }
        public GeneralMsgDto ReSendApprovalsForTheDistbutionService(string AcademicYearName)
        {
            return _context.ReSendApprovalsForTheDistbution(AcademicYearName);
        }
        public GeneralMsgDto ReSendApprovalsForTheDivisionService(string AcademicYearName)
        {
            return _context.ReSendApprovalsForTheDivision(AcademicYearName);
        }
        public GeneralMsgDto SendMarkToTheDepartmetnsHeadService(string DoctorId)
        {
            return _context.SendMarkToTheDepartmetnsHead(DoctorId);
        }
        public GeneralMsgDto ReSendMarkToTheDepartmetnsHeadService(string DoctorId)
        {
            return _context.ReSendMarkToTheDepartmetnsHead(DoctorId);
        
        }
        public GeneralMsgDto UpdateApprovalsForTheMark(UpdateApprovalsDto updateApprovalsDto, int markId)
        {
            return _context.UpdateApprovalsForTheMark(updateApprovalsDto , markId);

        }
        
        public List<GetApprovlesQ4Dto> GetListOfMarkForTheApprovlesForTheDepartmentHeadService(string DepartmentHeadId)
        {
            return _context.GetListOfMarkForTheApprovlesForTheDepartmentHead(DepartmentHeadId);

        }
        public List<GetApprovlesQ4Dto> MarkNotApprovedByTheDeprtmentHeadService()
        {
            return _context.MarkNotApprovedByTheDeprtmentHead();

        }
        public List<GetApprovlesQ4Dto> MarkApprovedByTheDeprtmentHeadService()
        {
            return _context.MarkApprovedByTheDeprtmentHead();

        }
        public List<GetApprovlesQ4Dto> GetListOfMarkForTheApprovlesForTheClinicalDepartmentDirectorService()
        {
            return _context.GetListOfMarkForTheApprovlesForTheClinicalDepartmentDirector();

        }
        public List<GetApprovlesQ4Dto> MarkNotApprovedByTheClinicalDepartmentDirectorService()
        {
            return _context.MarkNotApprovedByTheClinicalDepartmentDirector();

        }
        public List<GetApprovlesQ4Dto> MarkApprovedByTheClinicalDepartmentDirectorService()
        {
            return _context.MarkApprovedByTheClinicalDepartmentDirector();

        }

        public List<GetApprovlesQ4Dto> GetListOfMarkForTheApprovlesForTheDeanService()
        {
            return _context.GetListOfMarkForTheApprovlesForTheDean();
        }
        public List<GetApprovlesQ4Dto> MarkNotApprovedByTheDeanService()
        {
            return _context.MarkNotApprovedByTheDean();
        }
        public List<GetApprovlesQ4Dto> MarkApprovedByTheDeanService()
        {
            return _context.MarkApprovedByTheDean();
        }
        public List<GetApprovalsQ2Dto> GetListOfDistributionAfterTheApprovles(string AcademicYearName)
        {
            return _context.GetListOfDistributionAfterTheApprovles(AcademicYearName);   
        }
        public List<GetApprovalsQDto> GetListOfDivisionAfterTheApprovles(string AcademicYearName)
        {
            return _context.GetListOfDivisionAfterTheApprovles( AcademicYearName );
        }
        public List<GetMarkQ5Dto> GetMarkApprovedToTheDoctor(string doctorId)
        {
            return _context.GetMarkApprovedToTheDoctor(doctorId);

        }
        public List<GetMarkQ5Dto> GetMarkNotApprovedToTheDoctor(string doctorId)
        {
            return _context.GetMarkNotApprovedToTheDoctor(doctorId);

        }


    }
}
