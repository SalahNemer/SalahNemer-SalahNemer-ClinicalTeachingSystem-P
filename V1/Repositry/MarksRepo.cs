using BuildDB_Team.entitys;
using database.models;
using DataBase.DBcon;
using DevetionStudetns.DTO.AppointmentsDTO;
using DevetionStudetns.DTO.AttendanceDTO;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.NewFolder;
using FinalProject.DTO.QuestionnaireDTO;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using V1.DTO.MarkDTO;
using V1.Interface.IRepositry;
using V1.Mappers.MarkMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace V1.Repositry
{
    public class MarksRepo : IMark
    {
        private readonly DBC _context;
        public MarksRepo(DBC context)
        {
            _context = context;
        }

        public GeneralMsgDto AddMarks(List<AddMarkDto> addMarkList)
        {
            try
            {
                if (addMarkList == null || !addMarkList.Any())
                {
                    return new GeneralMsgDto(
                        IErrorMsgs.NOT_FOUND_Data,
                        "Not Found",
                        "Error"
                    );
                }

                List<string> errors = new List<string>();
                List<Marks> validMarks = new List<Marks>();

                foreach (var addMarkDto in addMarkList)
                {
                    var isDuplicate = _context.Marks.Any(p =>
                        p.DoctorId == addMarkDto.DoctorId &&
                        p.StudentId == addMarkDto.StudentId &&
                        p.CourseId == addMarkDto.CourseId &&
                        p.MarkType == addMarkDto.MarkType);

                    var studentExists = _context.students.Any(p => p.UserId == addMarkDto.StudentId);
                    var doctorExists = _context.doctors.Any(p => p.UserId == addMarkDto.DoctorId);
                    var courseExists = _context.Courses.Any(p => p.CouresId == addMarkDto.CourseId);

                    if (!studentExists || !doctorExists || !courseExists)
                    {
                        if (!studentExists)
                            errors.Add($"StudentId {addMarkDto.StudentId}: {IErrorMsgs.STUDENT_RECORD_NOT_FOUND}");
                        if (!doctorExists)
                            errors.Add($"DoctorId {addMarkDto.DoctorId}: {IErrorMsgs.DOCTOR_ID_NOT_FOUND_IN_DOCTOR_TABLE}");
                        if (!courseExists)
                            errors.Add($"CourseId {addMarkDto.CourseId}: {IErrorMsgs.COURSE_ID_NOT_FOUND_IN_COURSE_TABLE}");
                    }
                    else if (isDuplicate)
                    {
                        errors.Add($"Duplicate mark for StudentId {addMarkDto.StudentId}, CourseId {addMarkDto.CourseId}, MarkType {addMarkDto.MarkType}");
                    }
                    else
                    {
                        validMarks.Add(addMarkDto.AddMark());
                    }
                }

                if (validMarks.Any())
                {
                    try
                    {
                        _context.Marks.AddRange(validMarks);
                        _context.SaveChanges();

                        return new GeneralMsgDto(
                            SuccessfullyMsgs.SUCCESSFULLY_ADD_MARK,
                            "SUCCESSFULLY",
                            $"Successfully added {validMarks.Count} mark(s)"
                        );
                    }
                    catch (Exception)
                    {
                        return new GeneralMsgDto(
                            IErrorMsgs.Error,
                            "Database Error",
                            "Error while saving data"
                        );
                    }
                }

                return new GeneralMsgDto(
                    string.Join(" | ", errors),
                    "Validation Failed",
                    "Error"
                );

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public GeneralMsgDto DeleteMark(int MarkId)
        {
            
            if (MarkId == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.NOT_FOUND_Data,
                            "Not Fround",
                            "Error"
                            );
                return ErrorMsg;
            }
            else
            {
                var deleteMark = _context.Marks.FirstOrDefault(p => p.MarkId == MarkId);
                if (deleteMark == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.NOT_FOUND_MARK,
                            "Not Fround",
                            "Error"
                            );
                    return ErrorMsg;

                }
                else
                {
                    try
                    {
                        _context.Marks.Remove(deleteMark);
                        _context.SaveChanges();
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                        SuccessfullyMsgs.SUCCESSFUL_DELETE,
                        "SUCCESSFULLY",
                        "SUCCESSFULLY Deleted Mark"
                        );
                        return ErrorMsg;
                    }
                    catch (Exception e)
                    {
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                        IErrorMsgs.NOT_FOUND_Data,
                                                        "Not Fround",
                                                        "Error"
                                                        );
                            return ErrorMsg;
                        }
                    }
                }
            }
        }
        public GeneralMsgDto UpdateMark(UpdateMarkDto NewMarkDot, int MarkId)
        {
            if (NewMarkDot == null || MarkId == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.NOT_FOUND_Data,
                            "Not Fround",
                            "Error"
                            );
                return ErrorMsg;
            }
            else
            {
                var gitStudents = _context.students.Where(p => p.UserId == NewMarkDot.StudentId).ToList().Count;
                var gitCourse = _context.Courses.Where(p => p.CouresId == NewMarkDot.CourseId).ToList().Count;

                if (gitStudents == 0 && gitCourse == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                        IErrorMsgs.STUDENT_RECORD_NOT_FOUND +
                        " , " + IErrorMsgs.COURSE_ID_NOT_FOUND_IN_COURSE_TABLE,
                        "Not Fround",
                        "Error"
                        );
                    return ErrorMsg;
                }

                if (gitStudents == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                        IErrorMsgs.STUDENT_RECORD_NOT_FOUND,
                        "Not Fround",
                        "Error"
                        );
                    return ErrorMsg;
                }

                if (gitCourse == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                        IErrorMsgs.COURSE_ID_NOT_FOUND_IN_COURSE_TABLE,
                        "Not Fround",
                        "Error"
                        );
                    return ErrorMsg;
                }
                else
                {

                    var GetDublecatedData = _context.Marks.Where(p => p.StudentId == NewMarkDot.StudentId && p.Mark == NewMarkDot.Mark && p.CourseId == NewMarkDot.CourseId && p.MarkType == NewMarkDot.MarkType).ToList().Count;

                    if (GetDublecatedData == 0)
                    {
                        var getOldMark = _context.Marks.FirstOrDefault(p => p.MarkId == MarkId);
                        if (getOldMark != null)
                        {
                            try
                            {

                                getOldMark.StudentId = NewMarkDot.StudentId;
                                getOldMark.CourseId = NewMarkDot.CourseId;
                                getOldMark.Mark = NewMarkDot.Mark;
                                getOldMark.MarkType = NewMarkDot.MarkType;
                                getOldMark.Comments = NewMarkDot.Comments;
                                _context.SaveChanges();
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                SuccessfullyMsgs.SUCCESSFULLY_UPDATED_MARK,
                                "SUCCESSFULLY",
                                "SUCCESSFULLY Add Mark"
                                );
                                return ErrorMsg;
                            }
                            catch (Exception e)
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.Error,
                                "Not Fround",
                                "Error"
                                );
                                return ErrorMsg;
                            }

                        }

                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.NOT_FOUND_MARK,
                                "Not Fround",
                                "Not found any mark have this id "
                                );
                            return ErrorMsg;
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.Duplicate_Data,
                                "Duplicate Data",
                                "Duplicate Data "
                                );
                        return ErrorMsg;
                    }
                }
            }
        }

        public List<GetMarkQDto> GetMarkById(int markId)
        {
            string sql = @"
                                select 
                                m.MarkId ,
                                m.DoctorId,
                                m.StudentId,
                                e.FullName,
                                s.StudentLevel,
                                
                                c.CourseName,
                                m.MarkType,
                                m.Mark,
                                ISNULL (m.Comments,'')As Comments
                                from 
                                Marks m 
                                left join Students s on s.UserId = m.StudentId
                                left join Doctors d on d.UserId = m.DoctorId
                                left join Course c on c.CouresId = m.CourseId 
                                left join Users u on u.UserId = d.UserId
                                left join Users e on e.UserId = s.UserId
                                left join Divisions a on s.UserId = a.StudentId
                                left join SubGroup p on p.SubGroupId = a.SubGroupId
                                where 
                                m.MarkId =@markId
                                ";


            var result = _context.Database.SqlQueryRaw<GetMarkQDto>(
                            sql,
                            new SqlParameter("MarkId", markId)
                            ).ToList();
            return result;

        }
        public List<GetMarkQ1Dto> GetMarkByStudentsId(string StudentId)
        {
            string sql = @"
                                SELECT 
                                    m.MarkId,
                                    m.DoctorId,
                                    m.StudentId,
                                    e.FullName,
                                    s.StudentLevel,
                                    c.CourseName,
                                    m.MarkType,
                                    m.Mark,
                                    ISNULL(m.Comments, '') AS Comments,
                                    SUM(m.Mark) OVER (PARTITION BY m.StudentId, m.CourseId) AS TotalMark,
                                    COUNT(m.Mark) OVER (PARTITION BY m.StudentId, m.CourseId) AS MarkCount,
                                    AVG(m.Mark * 1.0) OVER (PARTITION BY m.StudentId, m.CourseId) AS AverageMark
                                FROM Marks m 
                                LEFT JOIN Students s ON s.UserId = m.StudentId
                                LEFT JOIN Doctors d ON d.UserId = m.DoctorId
                                LEFT JOIN Course c ON c.CouresId = m.CourseId 
                                LEFT JOIN Users u ON u.UserId = d.UserId
                                LEFT JOIN Users e ON e.UserId = s.UserId
                                LEFT JOIN Divisions a ON s.UserId = a.StudentId
                                LEFT JOIN SubGroup p ON p.SubGroupId = a.SubGroupId
                                WHERE m.StudentId = @StudentId
                                AND m.MarkStatus = 7


                                ";
            var result = _context.Database.SqlQueryRaw<GetMarkQ1Dto>(
                sql,
                new SqlParameter("StudentId", StudentId)
                ).ToList();
            return result;

        }
        public List<GetMarkQDto> GetMarkBySCorseIdAndAcadimicYear(string AcademicYearName, int CouresId)
        {
            string sql = @"
                                select 
                                m.MarkId ,
                                m.DoctorId,
                                m.StudentId,
                                e.FullName,
                                s.StudentLevel,
                                c.CourseName,
                                m.MarkType,
                                m.Mark,
                                ISNULL (m.Comments,'')As Comments
                                from 
                                Marks m 
                                left join Students s on s.UserId = m.StudentId
                                left join Doctors d on d.UserId = m.DoctorId
                                left join Course c on c.CouresId = m.CourseId 
                                left join Users u on u.UserId = d.UserId
                                left join Users e on e.UserId = s.UserId
                                left join Divisions a on s.UserId = a.StudentId
                                left join SubGroup p on p.SubGroupId = a.SubGroupId
                                left join MainGroup f on f.MainGroupId = p.MainGroupId  

                                where 
                                f.AcademicYearName = @AcademicYearName
                                c.CouresId =@CouresId
                                ";

            var result = _context.Database.SqlQueryRaw<GetMarkQDto>(
                sql,
                new SqlParameter("AcademicYearName", Convert.ToInt16(AcademicYearName)),
                new SqlParameter("CouresId", CouresId)
                ).ToList();
            return result;

        }
        public List<GetMarkQ3Dto> GetStudentsInOneCourseLastTheRotationByDoctorId(string DoctorId , string AcademicYear , string MarkType , int RotationId )
        {
            string sql = @"
                               	  		SELECT DISTINCT
                                            w.UserId AS StudentId, 
                                            w.FullName AS StudentName,
                                            w.Email,
                                            s.SubGroupSympole,
                                            p.MainGroupSympole,
                                            t.CouresId,
                                            t.CourseName,           
                                            t.CourseIevel
                                        FROM Distributions a
                                        JOIN SubGroup s ON s.SubGroupId = a.SubGroupId
                                        JOIN Divisions d ON d.SubGroupId = s.SubGroupId
                                        JOIN Students q ON q.UserId = d.StudentId	
                                        JOIN Users w ON q.UserId = w.UserId
                                        JOIN Appointments r ON r.AppointmentId = a.AppointmentId
                                        JOIN Doctors e ON e.UserId = a.DoctorId
                                        JOIN MainGroup p ON p.MainGroupId = s.MainGroupId
                                        JOIN Course t ON t.CouresId = a.CourseId
                                        JOIN Rotations rot ON rot.RotationId = r.RotationId
                                        JOIN Doctor_Course doco 
                                            ON doco.DoctorId = e.UserId 
                                            AND doco.Cours = a.CourseId
                                        WHERE 
                                            e.UserId = @DoctorId
                                            AND doco.CurrentAcademicYearName = p.AcademicYearName
                                            AND doco.CurrentAcademicYearName = @AcademicYear
                                            AND a.RotationId = @RotationId
                                            AND NOT EXISTS (
                                                SELECT 1 
                                                FROM Marks mar
                                                WHERE 
                                                    mar.DoctorId = a.DoctorId
                                                    AND mar.StudentId = q.UserId
                                                    AND mar.MarkType = @MarkType
                                                    AND a.RotationId = @RotationId
                                            )
	                                        select DISTINCT
	                                        di.DoctorId , 
	                                        u.UserId,
	                                        u.FullName
	                                        from Distributions di
	                                        join Divisions d on d.SubGroupId = di.SubGroupId
	                                        join Students st on st.UserId = d.StudentId
	                                        join Users u on u.UserId = st.UserId
	                                        join SubGroup s on s.SubGroupId = d.SubGroupId
	                                        join MainGroup m on m.MainGroupId = s.MainGroupId
	                                        where di.DoctorId = @DoctorId
	                                        and m.AcademicYearName = @AcademicYear
                                ";
            var result = _context.Database.SqlQueryRaw<GetMarkQ3Dto>(
                sql,
                new SqlParameter("DoctorId", DoctorId)
                , new SqlParameter("AcademicYear", AcademicYear)
                , new SqlParameter("RotationId" , RotationId)
                , new SqlParameter("MarkType",MarkType)
                ).ToList();
            return result;

        }

        public List<GetMarkQ4Dto> GetStudentsForTheUpdate(string DoctorId, string AcademicYear , int RotationId , string MarkType)
        {
            string sql = @"
                               	  		SELECT DISTINCT
                                             ae.MarkId,
                                            w.UserId AS StudentId, 
                                            w.FullName AS StudentName,
                                            w.Email,
                                            w.PhoneNumber,
                                            s.SubGroupSympole,
                                            p.MainGroupSympole,
                                            t.CouresId,
                                            t.CourseName,           
                                            t.CourseIevel,
	                                        ae.MarkType ,
	                                        ae.Mark
                                        FROM Distributions a
                                        JOIN SubGroup s ON s.SubGroupId = a.SubGroupId
                                        JOIN Divisions d ON d.SubGroupId = s.SubGroupId
                                        JOIN Students q ON q.UserId = d.StudentId	
                                        JOIN Users w ON q.UserId = w.UserId
                                        JOIN Appointments r ON r.AppointmentId = a.AppointmentId
                                        JOIN Doctors e ON e.UserId = a.DoctorId
                                        JOIN MainGroup p ON p.MainGroupId = s.MainGroupId
                                        JOIN Course t ON t.CouresId = a.CourseId
                                        JOIN Rotations rot ON rot.RotationId = r.RotationId
                                        JOIN Doctor_Course doco 
                                            ON doco.DoctorId = e.UserId 
                                            AND doco.Cours = a.CourseId
                                         JOIN Marks ae 
                                            ON ae.StudentId = q.UserId 
                                            AND ae.DoctorId = a.DoctorId 
                                            AND ae.CourseId = a.CourseId
                                        WHERE 
                                             e.UserId = @DoctorId
                                             and doco.CurrentAcademicYearName =  p.AcademicYearName
                                             and doco.CurrentAcademicYearName = @AcademicYear
	                                         and a.RotationId = @RotationId
                                             AND ae.MarkType = @MarkType


                                ";
            var result = _context.Database.SqlQueryRaw<GetMarkQ4Dto>(
                sql,
                new SqlParameter("DoctorId", DoctorId)
                , new SqlParameter("AcademicYear", AcademicYear)
                , new SqlParameter("RotationId", RotationId)
                , new SqlParameter("MarkType", MarkType)
                ).ToList();
            return result;

        }
        public List<GetMarkQ2Dto> GetSumOfTheWeeklyEvaluation(string StudentsId)
        {
            string sql = @"

 						   
                          WITH WeeklyScores AS (
                                SELECT
                                    w.StudentId,
                                    w.DoctorId,
                                    w.CourseId,
                                    ROUND(
                                        (SUM(w.AnswerTheQuestion * 1.0) / NULLIF(COUNT(DISTINCT app.WeekName), 0)) * (co.WeeklyRatingPercentage / 10.0),
                                        2
                                    ) AS FinalWeeklyScore
                                FROM WeeklyEvaluation w
                                JOIN Appointments app ON app.AppointmentId = w.AppointmentId
                                JOIN Course co ON co.CouresId = w.CourseId
                                WHERE w.StudentId = @StudentsId
                                GROUP BY w.StudentId, w.DoctorId, w.CourseId, co.WeeklyRatingPercentage
                            ),
                            TotalMarks AS (
                                SELECT
                                    m.StudentId,
                                    m.DoctorId,
                                    m.CourseId,
                                    SUM(m.Mark) AS TotalMark
                                FROM Marks m
                                WHERE m.StudentId = @StudentsId AND m.MarkStatus = 7
                                GROUP BY m.StudentId, m.DoctorId, m.CourseId
                            )

                            SELECT

                                u.FullName AS DoctorName,
                                tm.DoctorId,
                                e.FullName AS StudentName,
                                tm.StudentId,
	                            c.CouresId AS CourseId,
                                c.CourseCode,
                                c.CourseName,
                                ROUND(ISNULL(ws.FinalWeeklyScore, 0) + ISNULL(tm.TotalMark, 0), 2) AS CombinedFinalScore,
                                ISNULL(ws.FinalWeeklyScore, 0) AS FinalWeeklyScore
                            FROM TotalMarks tm
                            LEFT JOIN WeeklyScores ws 
                                ON ws.StudentId = tm.StudentId 
                                AND ws.CourseId = tm.CourseId 
                                AND ws.DoctorId = tm.DoctorId
                            LEFT JOIN Course c ON c.CouresId = tm.CourseId
                            LEFT JOIN Doctors d ON d.UserId = tm.DoctorId
                            LEFT JOIN Users u ON u.UserId = d.UserId
                            LEFT JOIN Students s ON s.UserId = tm.StudentId
                            LEFT JOIN Users e ON e.UserId = s.UserId

                                ";
          
           
            var result = _context.Database.SqlQueryRaw<GetMarkQ2Dto>(
            sql,
                new SqlParameter("StudentsId", StudentsId)
                ).ToList();
            return result;
        }
        public List<GetMarkQ6Dto> GetMarkForTheStudentByIdAndCourseId (string StudentsId , int CourseId)
        {
            string sql = @"

 						   
                                   WITH WeeklyScores AS(
                                  SELECT

                                      w.StudentId,
                                      w.DoctorId,
                                      w.CourseId,
                                      COUNT(DISTINCT app.WeekName) AS NumberOfWeeks,
                                      ROUND(
                                          (SUM(w.AnswerTheQuestion * 1.0) / COUNT(DISTINCT app.WeekName)) * (co.WeeklyRatingPercentage / 10.0),
                                          2
                                      ) AS FinalWeeklyScore

                                  FROM WeeklyEvaluation w

                                  JOIN Appointments app ON app.AppointmentId = w.AppointmentId

                                  JOIN Course co ON co.CouresId = w.CourseId

                                  WHERE w.StudentId = @StudentsId

                                  GROUP BY w.StudentId, w.DoctorId, w.CourseId, co.WeeklyRatingPercentage
                              )

                           SELECT
                               m.MarkId,
                               m.DoctorId,
                               u.FullName as DoctorName,
                               m.StudentId,
                               e.FullName as StudentName,
							   m.CourseId,
                               c.CourseCode,
                               c.CourseName,
                               m.MarkType,
                               m.Mark
   

                           
                           FROM Marks m
                           LEFT JOIN Students s ON s.UserId = m.StudentId
                           LEFT JOIN Doctors d ON d.UserId = m.DoctorId
                           LEFT JOIN Course c ON c.CouresId = m.CourseId
                           LEFT JOIN Users u ON u.UserId = d.UserId
                           LEFT JOIN Users e ON e.UserId = s.UserId
                           LEFT JOIN Divisions a ON s.UserId = a.StudentId
                           LEFT JOIN SubGroup p ON p.SubGroupId = a.SubGroupId
                           LEFT JOIN WeeklyScores ws ON ws.StudentId = m.StudentId AND ws.CourseId = m.CourseId

                           WHERE m.StudentId = @StudentsId
						   and m.CourseId= @CourseId 
                            AND m.MarkStatus = 7

                                ";


            var result = _context.Database.SqlQueryRaw<GetMarkQ6Dto>(
            sql,
                new SqlParameter("StudentsId", StudentsId),
                new SqlParameter("CourseId", CourseId)
                ).ToList();
            return result;
        }
    }
}
