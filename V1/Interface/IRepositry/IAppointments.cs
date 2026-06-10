using DataBase.entitys;
using DevetionStudetns.DTO.AppointmentsDTO;
using loginpage.DBcon;
using V1.DTO.AppointmentDTO;

namespace FinalProject.Interface.IRepositry
{
    public interface IAppointments
    {
        public GeneralMsgDto AddAppotments(AddAppointmentsDto addAppointmentsDto);
        public GeneralMsgDto DeleteAppointments(int AppointmentsId);
        public GeneralMsgDto UpdateAppointment(AddAppointmentsDto NewAppointmentData, int AppointmentId);
        public List<GetAppotmentsDto> getAppointmentsInTheOneSubGroup(int subGroupId, int RotationId);
        public List<GetAppotmentsDto> getAppointmentsInTheOneMainGroup(int MainGroupId, int RotationId);
        public List<GetAppointmentsQDto> GetAppointmentsByRotationId(int rotationId);
        public List<GetAppointmentsQDto> GetAppointmentsByAppointmentId(int AppointmentId);
        public List<GetAppointmentsQDto> GetAllAppointmentsByAppointment();
    }
}
