using database.models;
using DataBase.DBcon;
using DataBase.Entity;
using DevetionStudetns.DTO.AttendanceDTO;
using DevetionStudetns.Error.SuccessfullyMsg;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using testDtoAndmapper.Entity;
using V1.DTO.WeeklyEvaluationDTO;
using V1.Interface.IRepositry;
using DevetionStudetns.DTO.Repotrs;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace V1.Repositry
{
    public class WeeklyEvaluationRepo : IWeeklyEvaluationRepo
    {
        private readonly DBC _dbContext;
        public WeeklyEvaluationRepo(DBC dbContext)
        {
            _dbContext = dbContext;
        }

      public async Task<IEnumerable<GetWeeklyEvaluationDto>> GetAllWeeklyEvaluations()
        {

            var query = @"
                     SELECT 
                        we.WeeklyEvaluationId,
                        we.AppointmentId,
                        we.CourseId,
                        we.StudentId,
                        s.FullName AS StudentName,
                        we.DoctorId,
                        d.FullName AS DoctorName,
                        we.EvaluationFormId,
                        we.EvaluationQuestionId,
                        we.AnswerTheQuestion,
                        we.EntryDate
                    FROM WeeklyEvaluation we
                    JOIN Users s ON we.StudentId = s.UserId
                    JOIN Users d ON we.DoctorId = d.UserId";


            var result = await _dbContext.Database
                                            .SqlQueryRaw<GetWeeklyEvaluationDto>(query).ToListAsync();
            return result;
        }
        public async Task<WeeklyEvaluation?> GetWeeklyEvaluationById(int id)
        {
            return await _dbContext.WeeklyEvaluation.FindAsync(id);
        }
        private async Task<GeneralMsgDto?> ValidateWeeklyEvaluationAsync(WeeklyEvaluation weeklyEvaluation)
        {
            
            
            if (string.IsNullOrEmpty(weeklyEvaluation.StudentId) || !await (_dbContext.students.AnyAsync(u => u.UserId == weeklyEvaluation.StudentId))) /// انا هنا 
                return new GeneralMsgDto(IErrorMsgs.STUDENT_ID_NOT_FOUND_IN_STUDENTS_TABLE, "معرف الطالب غير صالح", "الرجاء إدخال معرف طالب صالح.");


            if (string.IsNullOrEmpty(weeklyEvaluation.DoctorId) || !await (_dbContext.doctors.AnyAsync(d => d.UserId == weeklyEvaluation.DoctorId)))
                return new GeneralMsgDto(IErrorMsgs.DOCTOR_ID_NOT_FOUND_IN_DOCTOR_TABLE, "معرف الطبيب غير صالح", "الرجاء إدخال معرف طبيب صالح.");


            if (weeklyEvaluation.EvaluationFormId == 0 || !await (_dbContext.EvaluationForm.AnyAsync(e => e.EvaluationFormId == weeklyEvaluation.EvaluationFormId)))
                return new GeneralMsgDto(IErrorMsgs.INVALID_EVALUATION_FORM, "خطأ في استمارة التقييم", "رقم استمارة التقييم  غير صالح أو غير موجود. يرجى التأكد من الرقم المدخل.");

            bool isWeeklyEvaluationExist = await _dbContext.WeeklyEvaluation.AnyAsync(w =>
                                                    w.StudentId == weeklyEvaluation.StudentId &&
                                                    w.DoctorId == weeklyEvaluation.DoctorId &&
                                                    w.EvaluationFormId == weeklyEvaluation.EvaluationFormId &&
                                                    w.EvaluationQuestionId == weeklyEvaluation.EvaluationQuestionId
                                                    );

            if (isWeeklyEvaluationExist)
                return new GeneralMsgDto(IErrorMsgs.WEEKLY_EVALUATION_ALREADY_EXIST, "خطأ ببيانات التقييم الأسبوعي", "التقييم الأسبوعي الذي تحاول إدخاله موجود مسبقاً, يرجى التأكد من البيانات المدخلة");
            

            if (weeklyEvaluation.EvaluationQuestionId == 0 || !await _dbContext.EvaluationQuestions.AnyAsync(e => e.QuestionId == weeklyEvaluation.EvaluationQuestionId))
                return new GeneralMsgDto(IErrorMsgs.INVALID_QUESTION_ID, "خطأ في معرف السؤال", "رقم السؤال غير صالح أو غير موجود. يرجى التأكد من الرقم المدخل.");

            

            return null;
        }
        public async Task<GeneralMsgDto> AddWeeklyEvaluation(WeeklyEvaluation weeklyEvaluation)
        {
            var validationError = await ValidateWeeklyEvaluationAsync(weeklyEvaluation);
            if (validationError != null) return validationError;
            try
            {

              string sql = @"
                                       INSERT INTO WeeklyEvaluation 
                                            (AnswerTheQuestion, StudentId, CourseId, DoctorId, AppointmentId, EvaluationQuestionId , EvaluationFormId, EntryDate) 
                                            VALUES 
                                            (
                                                {0}, 
                                                {1}, 
                                                (
                                                    SELECT TOP 1 d.CourseId 
                                                    FROM Distributions d
                                                    JOIN Appointments a ON d.AppointmentId = a.AppointmentId
                                                    WHERE 
                                                        d.DoctorId = {2} AND
                                                        CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Israel Standard Time' AS DATE)
                                                        BETWEEN CAST(a.EndSessionDate AS DATE) AND DATEADD(DAY, 2, CAST(a.EndSessionDate AS DATE))
                                                    ORDER BY a.EndSessionDate DESC  
                                                ),
                                                {2}, 
                                                (
                                                    SELECT TOP 1 s.AppointmentId 
                                                    FROM Appointments s 
                                                    WHERE CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Israel Standard Time' AS DATE)
                                                    BETWEEN CAST(s.EndSessionDate AS DATE) AND DATEADD(DAY, 2, CAST(s.EndSessionDate AS DATE))
                                                    ORDER BY s.EndSessionDate DESC
                                                ),
                                                {3}, 
                                                {4},
                                                CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Israel Standard Time' AS DATE)
                                        )

                                            ";
            await _dbContext.Database.ExecuteSqlRawAsync(sql,
                       weeklyEvaluation.AnswerTheQuestion,
                       weeklyEvaluation.StudentId,
                       weeklyEvaluation.DoctorId,
                       weeklyEvaluation.EvaluationQuestionId,
                       weeklyEvaluation.EvaluationFormId
            );
            await _dbContext.SaveChangesAsync();

            return new GeneralMsgDto(
                SuccessfullyMsgs.ADD_WEEKLY_EVALUATION_SUCCESS,  
                "تمت الإضافة",  
                "تم إضافة التقييم الأسبوعي للطالب بنجاح، وسيتم احتسابه ضمن التقييمات المقررة له.");
            }
            catch (Exception ex ){
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                         IErrorMsgs.Error,
                                         "Failed ",
                                         "Error Enter the corect  email "
                                         );
                return ErrorMsg;
            }
        }
        public async Task<GeneralMsgDto> UpdateWeeklyEvaluation(WeeklyEvaluation weeklyEvaluation)
        {
            var validationError = await ValidateWeeklyEvaluationAsync(weeklyEvaluation);
            if (validationError != null) return validationError;

            _dbContext.WeeklyEvaluation.Update(weeklyEvaluation);
            await _dbContext.SaveChangesAsync();
            return new GeneralMsgDto(SuccessfullyMsgs.UPDATE_WEEKLY_EVALUATION_SUCCESS, "تم التحديث", "تم تحديث التقييم الأسبوعي للطالب بنجاح.");
        }
        public async Task<GeneralMsgDto> DeleteWeeklyEvaluation(int id)
        {
            var eval = await _dbContext.WeeklyEvaluation.FindAsync(id);
            if (eval == null)
                return new GeneralMsgDto(IErrorMsgs.WEEKLY_EVALUATION_ALREADY_EXIST, "خطأ", "لا يوجد أي تقييم أسبوعي بهذا المعرف, يرجى التأكد من المعرف المدخل");


            _dbContext.WeeklyEvaluation.Remove(eval);
             await _dbContext.SaveChangesAsync();
            return new GeneralMsgDto(SuccessfullyMsgs.DELETE_WEEKLY_EVALUATION_SUCCESS, "تم الحذف", "تم حذف التقييم الأسبوعي للطالب بنجاح.");
        }
        public async Task<IEnumerable<GetEvaluationMarkByStudentIdQDto>> GetEvaluationMarkByStudentId(string studentId)
        {
            var query = @"
               SELECT 
                          m.StudentId,
                            e.FullName as StudentName,
                            m.DoctorId,
	                        do.FullName as DoctorName ,
	                        c.CourseCode,
                            c.CourseName,
	                        ef.EvaluationFormId ,
	                        eq.QuestionId,
	                        eq.QuestionText,
	                        eq.QuestionMark,
	                        m.AnswerTheQuestion,
                            SUM(m.AnswerTheQuestion) 
                                OVER (PARTITION BY m.StudentId, m.CourseId , m.DoctorId , m.AppointmentId) AS TotalMark,
	                        COUNT(m.AnswerTheQuestion) 
                                OVER (PARTITION BY m.StudentId, m.CourseId , m.DoctorId , m.AppointmentId) AS MarkCount
              FROM WeeklyEvaluation m 
              LEFT JOIN Students s ON s.UserId = m.StudentId
              LEFT JOIN Doctors d ON d.UserId = m.DoctorId
              LEFT JOIN Course c ON c.CouresId = m.CourseId 
              LEFT JOIN Users u ON u.UserId = d.UserId
              LEFT JOIN Users do ON do.UserId = d.UserId
              LEFT JOIN Users e ON e.UserId = s.UserId
              LEFT JOIN Divisions a ON s.UserId = a.StudentId
              LEFT JOIN SubGroup p ON p.SubGroupId = a.SubGroupId
              LEFT JOIN EvaluationQuestions eq ON eq.QuestionId = m.EvaluationQuestionId
              LEFT JOIN EvaluationForm ef ON ef.EvaluationFormId = m.EvaluationFormId
              WHERE m.StudentId = {0};";

            var result = await _dbContext.Database
                                            .SqlQueryRaw<GetEvaluationMarkByStudentIdQDto>(query, studentId)
                                        .ToListAsync();
            return result;

        }
       public async Task<IEnumerable<GetSubGroupForDoctorIdQDto>> GetAllSubGroupByDoctroId(string doctorId)
        {
            var query = @"
                                   SELECT 
                        w.UserId AS StudentId, 
                        w.FullName AS StudentName,
                        w.Email,
                        w.PhoneNumber,
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

                    -- استبعاد الطلاب الذين تم تقييمهم من هذا الطبيب خلال الأسبوع
                    LEFT JOIN WeeklyEvaluation ae 
                        ON ae.StudentId = q.UserId 
                        AND ae.DoctorId = e.UserId 
                        AND ae.AppointmentId = r.AppointmentId

                    WHERE 
                        CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Israel Standard Time' AS DATE)
                            BETWEEN CAST(r.EndSessionDate AS DATE) AND DATEADD(DAY, 2, CAST(r.EndSessionDate AS DATE))
                        AND e.UserId = {0}
                        AND ae.WeeklyEvaluationId IS NULL;";

            var result = await _dbContext.Database
                                            .SqlQueryRaw<GetSubGroupForDoctorIdQDto>(query, doctorId)
                                        .ToListAsync();
            return result;
        }
        public List<GetWeeklyEvaluationScoreDto> GetSumOfEvaluationWeekByStudentsId(string StudentsId)
        {
            var query = @"
                                 WITH WeeklyScores AS (
                                    SELECT 
                                        w.StudentId,
                                        w.DoctorId,
                                        w.CourseId,
                                        app.WeekName,
                                        SUM(w.AnswerTheQuestion) AS TotalScorePerWeek
                                    FROM WeeklyEvaluation w
                                    JOIN Appointments app ON app.AppointmentId = w.AppointmentId
                                    WHERE w.StudentId = @StudentsId
                                    GROUP BY w.StudentId, w.DoctorId, w.CourseId, app.WeekName
                                )

                                SELECT 
                                    ws.DoctorId,
                                    doc.FullName,
                                    ws.CourseId,
                                    co.CourseCode,
                                    co.CourseName,
                                    co.WeeklyRatingPercentage,
                                    COUNT(ws.WeekName) AS NumberOfWeeks,
                                    ROUND(
                                        (SUM(ws.TotalScorePerWeek * 1.0) / COUNT(ws.WeekName)) * (co.WeeklyRatingPercentage / 10.0),
                                        2
                                    ) AS FinalWeeklyScore
                                FROM WeeklyScores ws
                                JOIN Users doc ON doc.UserId = ws.DoctorId
                                JOIN Course co ON co.CouresId = ws.CourseId
                                GROUP BY 
                                    ws.DoctorId,
                                    doc.FullName,
                                    ws.CourseId,
                                    co.CourseCode,
                                    co.CourseName,
                                    co.WeeklyRatingPercentage;";

            var result =  _dbContext.Database.SqlQueryRaw<GetWeeklyEvaluationScoreDto>(query, new SqlParameter("StudentsId", StudentsId)).ToList();


            return result;
        }
    }
}
