using BuildDB_Team.entitys;
using V1.DTO.MarkDTO;

namespace V1.Mappers.MarkMapper
{
    public static class AddMarkMapper
    {
        public static Marks AddMark(this AddMarkDto AddMark)
        {
            return new Marks
            {
                Comments = AddMark.Comments,
                CourseId = AddMark.CourseId,
                DoctorId = AddMark.DoctorId,
                MarkType = AddMark.MarkType,
                StudentId = AddMark.StudentId,
                Mark = AddMark.Mark,
                EntryDate =  Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                MarkStatus = 2
            };
        }
    }
}
