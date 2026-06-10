using DataBase.DBcon;
using DevetionStudetns.Error.SuccessfullyMsg;
using FinalProject.DTO.AnswerTheEvaluationDTO;
using FinalProject.DTO.DoctorDto;
using FinalProject.Interface.IRepositry;
using FinalProject.Mappers.AnswerTheEvaluationMapper;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using V1.DTO.AnswerTheEvaluationDTO;
using V1.DTO.StudentsDTO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Data.SqlClient;
using DevetionStudetns.entitys;


namespace FinalProject.Repositry
{
    public class AnswerTheEvakluationReposoitry : IAnswerTheEvaluationRepo
    {
        readonly private DBC _db;
        public AnswerTheEvakluationReposoitry(DBC db)
        {
            _db = db;
        }

        public async Task<GeneralMsgDto> AddAnswerTheEvaluation(AddAnswerTheEvaluationDto evaluationAnswerDto)
        {
            var evaluatorPersonId = await _db.Users.FirstOrDefaultAsync(u1 => u1.UserId == evaluationAnswerDto.EvaluatorPersonId);
            var evaluatedPersonId = await _db.Users.FirstOrDefaultAsync(u2 => u2.UserId == evaluationAnswerDto.EvaluatedPersonId);
            var form = await _db.EvaluationForm.FirstOrDefaultAsync(f => f.EvaluationFormId == evaluationAnswerDto.EvaluationFormId);
            var question = await _db.EvaluationQuestions.FirstOrDefaultAsync(q => q.QuestionId == evaluationAnswerDto.QuestionId);
            try
            {
                if (evaluatorPersonId != null)
                {
                    if (evaluatedPersonId != null)
                    {
                        if (form != null)
                        {
                            if (question != null)
                            {
                                var GetAppoutmentId = @"
                                select 
                                    a.AppointmentId
                                    from Appointments a 
                                where       
                                    CAST(GETDATE() AS DATE) BETWEEN 
                                    CAST(a.EndSessionDate AS DATE) 
                                AND 
                                    DATEADD(DAY, 2, CAST(a.EndSessionDate AS DATE))";
                                int AppotmentResult = await _db.Appointment
                                    .FromSqlRaw(GetAppoutmentId)
                                    .Select(a => a.AppointmentId)
                                    .FirstOrDefaultAsync();
                                if (AppotmentResult != 0)
                                {
                                    var answer = new AnswerTheEvaluation
                                    {
                                        EvaluatorPersonId = evaluationAnswerDto.EvaluatorPersonId,
                                        EvaluatedPersonId = evaluationAnswerDto.EvaluatedPersonId,
                                        EvaluationFormId = evaluationAnswerDto.EvaluationFormId,
                                        QuestionId = evaluationAnswerDto.QuestionId,
                                        TheAnswer = evaluationAnswerDto.TheAnswer,
                                        Appointmentid = AppotmentResult,
                                        DateTimeAnswer = DateTime.Now
                                    };
                                    _db.AnswerTheEvaluation.Add(answer);
                                    await _db.SaveChangesAsync();
                                    GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                        SuccessfullyMsgs.Added_successfully,
                                                        "Success",
                                                        "Success"
                                                      );
                                    return SucMsg;
                                }
                                else
                                {
                                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.Error,
                                    "Enter the requird filled",
                                    "Ther is not any data to Update it "
                                          );
                                    return ErrorMsg;
                                }
                            }
                            else
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.The_Question_You_Want_Does_Not_Exist,
                                    "Enter the requird filled",
                                    "Ther is not any data to Update it "
                                          );
                                return ErrorMsg;
                            }
                        }
                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.The_Form_You_Want_Does_Not_Exist,
                                "Enter the requird filled",
                                "Ther is not any data to Update it "
                                      );
                            return ErrorMsg;
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.The_ID_Of_The_Person_To_Be_Evaluated_Was_Not_Found,
                            "Enter the requird filled",
                            "Ther is not any data to Update it "
                                  );
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                        IErrorMsgs.The_ID_Of_The_Person_Doing_The_Evaluation_Does_Not_Exist,
                        "Enter the requird filled",
                        "Ther is not any data to Update it "
                      );
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }
        public async Task<GeneralMsgDto> AddAnswerTheEvaluationForTheDepartmentHead(AddAnswerTheEvaluationDto evaluationAnswerDto)
        {
            var evaluatorPersonId = await _db.Users.FirstOrDefaultAsync(u1 => u1.UserId == evaluationAnswerDto.EvaluatorPersonId);
            var evaluatedPersonId = await _db.Users.FirstOrDefaultAsync(u2 => u2.UserId == evaluationAnswerDto.EvaluatedPersonId);
            var form = await _db.EvaluationForm.FirstOrDefaultAsync(f => f.EvaluationFormId == evaluationAnswerDto.EvaluationFormId);
            var question = await _db.EvaluationQuestions.FirstOrDefaultAsync(q => q.QuestionId == evaluationAnswerDto.QuestionId);
            try
            {
                if (evaluatorPersonId != null)
                {
                    if (evaluatedPersonId != null)
                    {
                        if (form != null)
                        {
                            if (question != null)
                            {

                                var answer = new AnswerTheEvaluation
                                {
                                    EvaluatorPersonId = evaluationAnswerDto.EvaluatorPersonId,
                                    EvaluatedPersonId = evaluationAnswerDto.EvaluatedPersonId,
                                    EvaluationFormId = evaluationAnswerDto.EvaluationFormId,
                                    QuestionId = evaluationAnswerDto.QuestionId,
                                    TheAnswer = evaluationAnswerDto.TheAnswer,
                                    DateTimeAnswer = DateTime.Now
                                };
                                _db.AnswerTheEvaluation.Add(answer);
                                await _db.SaveChangesAsync();
                                GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                    SuccessfullyMsgs.Added_successfully,
                                                    "Success",
                                                    "Success"
                                                  );
                                return SucMsg;

                            }
                            else
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.The_Question_You_Want_Does_Not_Exist,
                                    "Enter the requird filled",
                                    "Ther is not any data to Update it "
                                          );
                                return ErrorMsg;
                            }
                        }
                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.The_Form_You_Want_Does_Not_Exist,
                                "Enter the requird filled",
                                "Ther is not any data to Update it "
                                      );
                            return ErrorMsg;
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.The_ID_Of_The_Person_To_Be_Evaluated_Was_Not_Found,
                            "Enter the requird filled",
                            "Ther is not any data to Update it "
                                  );
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                        IErrorMsgs.The_ID_Of_The_Person_Doing_The_Evaluation_Does_Not_Exist,
                        "Enter the requird filled",
                        "Ther is not any data to Update it "
                      );
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        public async Task<GeneralMsgDto> DeleteAnswerTheEvaluation(int id)
        {
            var Answer =await _db.AnswerTheEvaluation.FirstOrDefaultAsync(u => u.AnswerId == id);
            try
            {
                if (Answer != null)
                {
                    _db.AnswerTheEvaluation.Remove(Answer);
                    _db.SaveChanges();
                    GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                  SuccessfullyMsgs.The_operation_was_completed_successfully,
                                                  "Success",
                                                  "Success"
                                                  );
                    return SucMsg;
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                  IErrorMsgs.The_Question_You_Want_Does_Not_Exist,
                                                  "Enter the requird filled",
                                                  "Ther is not any data to Update it "
                                                  );
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى ", ex);
            }
        }
        public async Task<List<GetAnswerTheEvaluationQDto>> GetAnswerTheEvaluaion()
        {
            try
            {
                string sql = @"	 
				SELECT 
						A.AnswerId,
						A.EvaluatorPersonId,
						EvalUser.FirstName AS EvaluatorFirstName,
						EvalUser.LastName AS EvaluatorLastName,
						EvalUser.FullName AS EvaluatorFullName,
						A.EvaluatedPersonId,
						EvaldUser.firstName AS EvaluatedfirstName,
						EvaldUser.lastName AS EvaluatedlastName,
						EvaldUser.fullName AS EvaluatedfullName,
						A.EvaluationFormId,
						EF.EvaluationFormType,
						A.QuestionId,
						EQ.QuestionText,
						EQ.QuestionType,
						A.TheAnswer,
                        A.DateTimeAnswer
				FROM AnswerTheEvaluation A
				JOIN Users EvalUser ON A.EvaluatorPersonId = EvalUser.UserId
				JOIN Users EvaldUser ON A.EvaluatedPersonId = EvaldUser.UserId
				JOIN EvaluationForm EF ON A.EvaluationFormId = EF.EvaluationFormId
				JOIN EvaluationQuestions EQ ON A.QuestionId = EQ.QuestionId;
               ";
                var result = await _db.Database.SqlQueryRaw<GetAnswerTheEvaluationQDto>(sql).ToListAsync();
                if (result == null)
                    return null;
                return result;
            }
            catch ( Exception ex )
            {
                throw new Exception("خطأ اثناء تنفيذ الاستعلام يرجى التاكد من الاستعلام \n", ex);
            }
        }
        public async Task<GetAnswerTheEvaluationQDto> GetAnswerTheEvaluaionByAnswerId(int id)
        {
            var answerid=await _db.AnswerTheEvaluation.FirstOrDefaultAsync(a=>a.AnswerId==id);
            try
            {
                if (answerid != null)
                {
                    string sql = @"	 
					SELECT 
						A.AnswerId,
						A.EvaluatorPersonId,
						EvalUser.FirstName AS EvaluatorFirstName,
						EvalUser.LastName AS EvaluatorLastName,
						EvalUser.FullName AS EvaluatorFullName,
						A.EvaluatedPersonId,
						EvaldUser.firstName AS EvaluatedfirstName,
						EvaldUser.lastName AS EvaluatedlastName,
						EvaldUser.fullName AS EvaluatedfullName,
						A.EvaluationFormId,
						EF.EvaluationFormType,
						A.QuestionId,
						EQ.QuestionText,
						EQ.QuestionType,
						A.TheAnswer,
                        A.DateTimeAnswer
				FROM AnswerTheEvaluation A
				JOIN Users EvalUser ON A.EvaluatorPersonId = EvalUser.UserId
				JOIN Users EvaldUser ON A.EvaluatedPersonId = EvaldUser.UserId
				JOIN EvaluationForm EF ON A.EvaluationFormId = EF.EvaluationFormId
				JOIN EvaluationQuestions EQ ON A.QuestionId = EQ.QuestionId
				WHERE A.AnswerId=@p0
";
                    var result = await _db.Database.SqlQueryRaw<GetAnswerTheEvaluationQDto>(sql, id).FirstOrDefaultAsync();
                    if (result == null)
                        return null;
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch ( Exception ex )
            {
                throw new Exception("خطأ اثناء تنفيذ الاستعلام يرجى التاكد من الاستعلام \n", ex);
            }
        }

        public async Task<List<GetDataDoctorsByStudentIdQDTO>> ShowDataDoctorByStudentId(string studentid)
        {
            try
            {
                var GetAppoutmentId = @"
                                	  SELECT 
            a.AppointmentId
	        FROM Appointments a 
        WHERE 
         CAST(a.EndSessionDate AS DATE) BETWEEN 
        CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Israel Standard Time' AS DATE) 
        AND 
        DATEADD(DAY, 2, CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Israel Standard Time' AS DATE)) 
                            ";

var AppotmentResult = await _db.Appointment
                    .FromSqlRaw(GetAppoutmentId)
                    .Select(a => a.AppointmentId)
                    .FirstOrDefaultAsync();


                var Appointment = await _db.AnswerTheEvaluation.FirstOrDefaultAsync(p => p.EvaluatorPersonId == studentid && p.Appointmentid == AppotmentResult);       
                if (Appointment == null)
                {
                    string sql = @"	 
                        SELECT 
                            u.UserId,
                            u.FullName,
                            a.AppointmentId,
                            a.StartSessionDate,
                            a.EndSessionDate
                        FROM Doctors d 
                        JOIN Users u ON u.UserId = d.UserId
                        JOIN Distributions n ON n.DoctorId = d.UserId 
                        JOIN Appointments a ON a.AppointmentId = n.AppointmentId
                        JOIN SubGroup s ON s.SubGroupId = n.SubGroupId 
                        JOIN Divisions w ON w.SubGroupId = s.SubGroupId
                        JOIN Students t ON t.UserId = w.StudentId 
                        WHERE  
                            t.UserId = @p0
                            and
                             CAST(a.EndSessionDate AS DATE) BETWEEN 
        CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Israel Standard Time' AS DATE) 
        AND 
        DATEADD(DAY, 2, CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Israel Standard Time' AS DATE)) 

   
               ";
                    var result = await _db.Database.SqlQueryRaw<GetDataDoctorsByStudentIdQDTO>(sql, studentid).ToListAsync();
                    if (result == null)
                        return null;
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }

        }
        public List<GetAllDoctorInTheSameDepartmentFroEvaluationQDto> ShowAllDoctorInTheDepartmentByDepartmentHeadId(string DepartmentHeadId)
        {

            string sql = @"	
	                    SELECT DISTINCT
            doc.UserId, 
            do.FullName AS DoctorName,
            d.DepartmentName,
		    ho.HospitalName  
            FROM Doctors doc
            JOIN Users do ON do.UserId = doc.UserId
            JOIN Department d ON d.DepartmentId = doc.DepartmentId  
            JOIN Distributions dis ON dis.DoctorId = doc.UserId
            JOIN Rotations app ON app.RotationId = dis.RotationId
		    join Hospitals ho on ho.HospitalId = doc.HospitalId
            WHERE  
            d.DepartmentId = (SELECT head.DepartmentId FROM Doctors head WHERE head.UserId = @DepartmentHeadId)
            AND doc.UserId <> @DepartmentHeadId
            AND do.AccountStatus = 1
	        and
	        CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Israel Standard Time' AS DATE)
            BETWEEN CAST(app.EndRotationDate AS DATE) AND DATEADD(DAY, 10, CAST(app.EndRotationDate AS DATE))
            AND NOT EXISTS (
        SELECT 1 
        FROM AnswerTheEvaluation we
        WHERE we.EvaluatedPersonId = doc.UserId
          AND we.EvaluatorPersonId = @DepartmentHeadId
          AND CAST(we.DateTimeAnswer AS DATE) BETWEEN CAST(app.StartRotationDate AS DATE) AND DATEADD(DAY, 7, CAST(app.EndRotationDate AS DATE))
    );

               ";
            var result = _db.Database.SqlQueryRaw<GetAllDoctorInTheSameDepartmentFroEvaluationQDto>(sql,
                new SqlParameter("DepartmentHeadId", DepartmentHeadId)).ToList();
            if (result == null)
                return null;
            return result;
        }

    }
}
