using DataBase.entitys;
using DevetionStudetns.DTO.DistributionsDTO;

namespace DevetionStudetns.Mappers.DistubutionsMapper
{
    public static class AddDistributionMapper
    {
        public static Distribution AddDistribution (this AddDistibutionsDto addDistibutions)
        {
            return new Distribution
            {
                CourseId = addDistibutions.CourseId,
                AppointmentId = addDistibutions.AppointmentId,
                SubGroupId = addDistibutions.SubGroupId,
                DoctorId = addDistibutions.DoctorId,
                RotationId = addDistibutions.RotationId,
                DistributionStatus = 1
            }; 
        }
    }
}
