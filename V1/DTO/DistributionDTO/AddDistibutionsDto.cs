using DataBase.entitys;
using DevetionStudetns.NewFolder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using testDtoAndmapper.Entity;

namespace DevetionStudetns.DTO.DistributionsDTO
{
    public class AddDistibutionsDto
    {
        [Required(ErrorMessage = "SubGroupId is required.")]
        public int SubGroupId { get; set; }
        [Required(ErrorMessage = "DoctorId is required.")]
        public string DoctorId { get; set; }
        [Required(ErrorMessage = "CourseId is required.")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "RotationId is required.")]
        public int RotationId { get; set; }
        [Required(ErrorMessage = "AppointmentId is required.")]
        public int AppointmentId { get; set; }
    }
}
