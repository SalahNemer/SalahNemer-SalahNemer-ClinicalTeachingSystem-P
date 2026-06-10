using DataBase.entitys;
using DevetionStudetns.DTO.AppointmentsDTO;
using System.Runtime.CompilerServices;

namespace DevetionStudetns.Mappers.AppointmentsMapper
{
    public static class AddAppoinmentsMappr
    {
        public static Appointment AddAppointments (this AddAppointmentsDto AppointmentsDto)
        {
            return new Appointment
            {
                RotationId = AppointmentsDto.RotationId,
                StartSessionDate = AppointmentsDto.StartSessionDate,
                EndSessionDate = AppointmentsDto.EndSessionDate,
                SessionStartTime = AppointmentsDto.SessionStartTime,
                SessionEndTime = AppointmentsDto.SessionEndTime,
                WeekName = AppointmentsDto.WeekName,
            };  
        }
    }
}
