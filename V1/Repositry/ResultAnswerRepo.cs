using DataBase.DBcon;
using DevetionStudetns.DTO.StudentsDTO;
using DevetionStudetns.entitys;
using FastReport;
using FinalProject.Service;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using testDtoAndmapper.Entity;
using V1.DTO.AnswerTheEvaluationDTO;
using V1.DTO.ResultAnswerTheEvaluationS_D;
using V1.DTO.StudentsDTO;
using V1.Interface.IRepositry;

namespace V1.Repositry
{
    public class ResultAnswerRepo : IResultAnswer
    {
        private readonly DBC _db;
        public ResultAnswerRepo(DBC db)
        {
            _db = db;
        }
        public async Task<List<GetValueAnswerTheEvaluationQDto>> GetResultAnswerTheEvaluationByFormId(int formid)
        {
            var FormID =await _db.AnswerTheEvaluation.FirstOrDefaultAsync(f => f.EvaluationFormId == formid);
            try
            {
                if (FormID != null)
                {
                    string sql = @"	 
			SELECT 
						A.AnswerId,
						A.EvaluatorPersonId,
						EvalUser.FirstName AS EvaluatorFirstName,
						EvalUser.LastName AS EvaluatorLastName,
						EvalUser.FullName AS EvaluatorFullName,
						A.EvaluatedPersonId,
						EvaldUser.firstName AS EvaluatedFirstName,
						EvaldUser.lastName AS EvaluatedLastName,
						EvaldUser.fullName AS EvaluatedFullName,
						A.EvaluationFormId,
						EF.EvaluationFormType,
						A.QuestionId,
						EQ.QuestionText,
						EQ.QuestionType,
						A.TheAnswer,
						    CASE 
								WHEN A.TheAnswer = '20' THEN 20
								WHEN A.TheAnswer = '40' THEN 40
								WHEN A.TheAnswer = '60' THEN 60
								WHEN A.TheAnswer = '80' THEN 80
								WHEN A.TheAnswer = '100' THEN 100
								WHEN A.TheAnswer = '15' THEN 15
								WHEN A.TheAnswer = '5' THEN 5
								WHEN A.TheAnswer = '12' THEN 12
								WHEN A.TheAnswer = '2' THEN 2
								WHEN A.TheAnswer = '4' THEN 4
								WHEN A.TheAnswer = '8' THEN 8
								WHEN A.TheAnswer = '6' THEN 6
								WHEN A.TheAnswer = '3' THEN 3
								WHEN A.TheAnswer = '1' THEN 1
								WHEN A.TheAnswer = '10' THEN 10

								ELSE 0  -- قيمة افتراضية إذا لم تكن الإجابة مطابقة لأي شرط
							END AS Value
				FROM AnswerTheEvaluation A
				JOIN Users EvalUser ON A.EvaluatorPersonId = EvalUser.UserId
				JOIN Users EvaldUser ON A.EvaluatedPersonId = EvaldUser.UserId
				JOIN EvaluationForm EF ON A.EvaluationFormId = EF.EvaluationFormId
				JOIN EvaluationQuestions EQ ON A.QuestionId = EQ.QuestionId
				where A.EvaluationFormId=@p0;
               ";
                    var result = await _db.Database.SqlQueryRaw<GetValueAnswerTheEvaluationQDto>(sql, formid).ToListAsync();
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

        public async Task<List<GetTotalValueEvaluationByFormIDQDTO>> GetTotalValueEvaluationByFormId(int formid)
        {
            var FormID = await _db.AnswerTheEvaluation.FirstOrDefaultAsync(f => f.EvaluationFormId == formid);
            try
            {
                if (FormID != null)
                {
                    string sql = @"	 
	                    SELECT 
			                    A.EvaluationFormId,
	    	                    EF.EvaluationFormType,
			                    A.QuestionId,
			                    EQ.QuestionText,
                        COUNT(A.QuestionId) AS CountAnswerTheQuestion,  -- عدد الأسئلة
                        SUM(
                            CASE 
									WHEN A.TheAnswer = '20' THEN 20
								WHEN A.TheAnswer = '40' THEN 40
								WHEN A.TheAnswer = '60' THEN 60
								WHEN A.TheAnswer = '80' THEN 80
								WHEN A.TheAnswer = '100' THEN 100
								WHEN A.TheAnswer = '15' THEN 15
								WHEN A.TheAnswer = '5' THEN 5
								WHEN A.TheAnswer = '12' THEN 12
								WHEN A.TheAnswer = '2' THEN 2
								WHEN A.TheAnswer = '4' THEN 4
								WHEN A.TheAnswer = '8' THEN 8
								WHEN A.TheAnswer = '6' THEN 6
								WHEN A.TheAnswer = '3' THEN 3
				                WHEN A.TheAnswer = '1' THEN 1
								WHEN A.TheAnswer = '10' THEN 10
								ELSE 0 
                            END
                        ) AS TotalValue,  -- مجموع القيم
    
                        CASE 
                            WHEN COUNT(A.QuestionId) > 0 THEN 
                                SUM(
                                    CASE 
										WHEN A.TheAnswer = '20' THEN 20
								WHEN A.TheAnswer = '40' THEN 40
								WHEN A.TheAnswer = '60' THEN 60
								WHEN A.TheAnswer = '80' THEN 80
								WHEN A.TheAnswer = '100' THEN 100
								WHEN A.TheAnswer = '15' THEN 15
								WHEN A.TheAnswer = '5' THEN 5
								WHEN A.TheAnswer = '12' THEN 12
								WHEN A.TheAnswer = '2' THEN 2
								WHEN A.TheAnswer = '4' THEN 4
								WHEN A.TheAnswer = '8' THEN 8
								WHEN A.TheAnswer = '6' THEN 6
								WHEN A.TheAnswer = '3' THEN 3
				                WHEN A.TheAnswer = '1' THEN 1
			    				WHEN A.TheAnswer = '10' THEN 10
								ELSE 0 
                                    END
                                ) / COUNT(A.QuestionId)  -- حساب المتوسط
                            ELSE 0 
                        END AS AverageValue  -- المعدل

                    FROM AnswerTheEvaluation A
                    JOIN Users EvalUser ON A.EvaluatorPersonId = EvalUser.UserId
                    JOIN Users EvaldUser ON A.EvaluatedPersonId = EvaldUser.UserId
                    JOIN EvaluationForm EF ON A.EvaluationFormId = EF.EvaluationFormId
                    JOIN EvaluationQuestions EQ ON A.QuestionId = EQ.QuestionId
                    WHERE A.EvaluationFormId = @p0
                    GROUP BY A.EvaluationFormId,EF.EvaluationFormType,A.QuestionId,EQ.QuestionText;
               ";
                    var result = await _db.Database.SqlQueryRaw<GetTotalValueEvaluationByFormIDQDTO>(sql, formid).ToListAsync();
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

        public async Task<List<GetDataValueAnswerTheEvaluationQDto>> ShowTotalEvaluationDoctorByForm(int formid, string doctorid)
        {
            var FormID =await _db.AnswerTheEvaluation.FirstOrDefaultAsync(f => f.EvaluationFormId == formid && f.EvaluatedPersonId == doctorid);
            try
            {
                if (FormID != null)
                {
                    string sql = @"	 
SELECT 
    A.EvaluationFormId,
    EF.EvaluationFormType,
    COUNT(DISTINCT A.QuestionId) AS CountQuestion,  -- عدد الأسئلة
    COUNT(DISTINCT A.EvaluatorPersonId) AS NumberOfStudent,  -- عدد الطلاب الذين قاموا بالتقييم
    SUM(EQ.QuestionMark) AS MaxPossibleScore,  -- الحد الأقصى الممكن للنقاط لكل الأسئلة
    SUM(
        CASE 
            WHEN A.TheAnswer = '20' THEN 20
            WHEN A.TheAnswer = '40' THEN 40
            WHEN A.TheAnswer = '60' THEN 60
            WHEN A.TheAnswer = '80' THEN 80
            WHEN A.TheAnswer = '100' THEN 100
            WHEN A.TheAnswer = '15' THEN 15
            WHEN A.TheAnswer = '5' THEN 5
            WHEN A.TheAnswer = '12' THEN 12
            WHEN A.TheAnswer = '2' THEN 2
            WHEN A.TheAnswer = '4' THEN 4
            WHEN A.TheAnswer = '8' THEN 8
            WHEN A.TheAnswer = '6' THEN 6
            WHEN A.TheAnswer = '3' THEN 3
            WHEN A.TheAnswer = '1' THEN 1
            WHEN A.TheAnswer = '10' THEN 10
            ELSE 0 
        END
    ) AS TotalValue,  -- مجموع القيم

    CASE 
        WHEN SUM(EQ.QuestionMark) > 0 THEN 
            (
                SUM(
                    CASE 
                        WHEN A.TheAnswer = '20' THEN 20
                        WHEN A.TheAnswer = '40' THEN 40
                        WHEN A.TheAnswer = '60' THEN 60
                        WHEN A.TheAnswer = '80' THEN 80
                        WHEN A.TheAnswer = '100' THEN 100
                        WHEN A.TheAnswer = '15' THEN 15
                        WHEN A.TheAnswer = '5' THEN 5
                        WHEN A.TheAnswer = '12' THEN 12
                        WHEN A.TheAnswer = '2' THEN 2
                        WHEN A.TheAnswer = '4' THEN 4
                        WHEN A.TheAnswer = '8' THEN 8
                        WHEN A.TheAnswer = '6' THEN 6
                        WHEN A.TheAnswer = '3' THEN 3
                        WHEN A.TheAnswer = '1' THEN 1
                        WHEN A.TheAnswer = '10' THEN 10
                        ELSE 0 
                    END
                ) * 1.0 / SUM(EQ.QuestionMark)
            ) * 100
        ELSE 0 
    END AS PercentageValue  -- نسبة التقييم من 100

FROM AnswerTheEvaluation A
JOIN Users EvalUser ON A.EvaluatorPersonId = EvalUser.UserId
JOIN Users EvaldUser ON A.EvaluatedPersonId = EvaldUser.UserId
JOIN EvaluationForm EF ON A.EvaluationFormId = EF.EvaluationFormId
JOIN EvaluationQuestions EQ ON A.QuestionId = EQ.QuestionId
WHERE A.EvaluationFormId = @p0 AND A.EvaluatedPersonId = @p1
GROUP BY A.EvaluationFormId, EF.EvaluationFormType


               ";
                    var result = await _db.Database.SqlQueryRaw<GetDataValueAnswerTheEvaluationQDto>(sql, formid, doctorid).ToListAsync();
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
    }
}
