using DataBase.entitys;
using DevetionStudetns.DTO.AppointmentsDTO;

namespace DevetionStudetns.Mappers.AppointmentsMapper
{
    public static class GetAppoitmentsMapper
    {
        public static AddAppointmentsDto AddAppointments(this Appointment AppointmentsDto)
        {
            return new AddAppointmentsDto
            {
                StartSessionDate = AppointmentsDto.StartSessionDate,
                RotationId = AppointmentsDto.RotationId,
                WeekName = AppointmentsDto.WeekName,
                EndSessionDate = AppointmentsDto.EndSessionDate,
                SessionStartTime = AppointmentsDto.SessionStartTime,
                SessionEndTime = AppointmentsDto.SessionEndTime,
            };
        }
    }
}
