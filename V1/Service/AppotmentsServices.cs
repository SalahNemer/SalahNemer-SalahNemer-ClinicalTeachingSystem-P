using DataBase.entitys;
using DevetionStudetns.DTO.AppointmentsDTO;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using V1.DTO.AppointmentDTO;

namespace DevetionStudetns.Service
{
    public class AppotmentsServices
    {
        private readonly IAppointments _context;
        public AppotmentsServices(IAppointments context)
        {
            _context = context;
        }

        public GeneralMsgDto AddAppotmentsService(AddAppointmentsDto addAppointmentsDto)
        {
            return _context.AddAppotments(addAppointmentsDto);
        }
        public GeneralMsgDto DeleteAppointmentsService(int AppointmentsId)
        {
            return _context.DeleteAppointments(AppointmentsId);
        }
        public GeneralMsgDto UpdateAppointmentService(AddAppointmentsDto NewAppointmentData, int AppointmentId)
        {
            return _context.UpdateAppointment(NewAppointmentData, AppointmentId);
        }
        public List<GetAppotmentsDto> getAppointmentsInTheOneSubGroupService(int subGroupId, int RotationId)
        {
            return _context.getAppointmentsInTheOneSubGroup(subGroupId, RotationId);
        }
        public List<GetAppotmentsDto> getAppointmentsInTheOneMainGroupService(int MainGroupId, int RotationId)
        {
            return _context.getAppointmentsInTheOneMainGroup(MainGroupId, RotationId);
        }
        public List<GetAppointmentsQDto> GetAppointmentsByRotationIdService(int rotationId)
        {
            return _context.GetAppointmentsByRotationId(rotationId);
        }
        public List<GetAppointmentsQDto> GetAppointmentsByAppointmentIdService(int AppointmentId)
        {
            return _context.GetAppointmentsByAppointmentId(AppointmentId);
        }
        public List<GetAppointmentsQDto> GetAllAppointmentsByAppointmentService()
        {
            return _context.GetAllAppointmentsByAppointment();
        }



    }
}
