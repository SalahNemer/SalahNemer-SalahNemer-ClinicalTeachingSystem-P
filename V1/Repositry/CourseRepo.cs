using database.models;
using DataBase.DBcon;
using DevetionStudetns.DTO;
using DevetionStudetns.DTO.Hospital;
using DevetionStudetns.Entity;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Interface;
using DevetionStudetns.Mappers.CourseMappier;
using DevetionStudetns.NewFolder;
using FinalProject.DTO.CourseDTO;
using FinalProject.DTO.DoctorDto;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using V1.DTO.CourseDTO;


namespace DevetionStudetns.Repositry.CourseRepostry
{
    public class CourseRepo : ICourse
    {
        private readonly DBC _context;
        public CourseRepo(DBC context)
        {
            _context = context;
        }
        public async Task<GeneralMsgDto> AddCourse(CourseDto courses)
        {
            var CourseName = await _context.Courses.FirstOrDefaultAsync(d => d.CourseName == courses.CourseName);
            var CourseCode = await _context.Courses.FirstOrDefaultAsync(cc => cc.CourseCode == courses.CourseCode);
            try
            {
                if (CourseName == null)
                {
                    if (CourseCode == null)
                    {

                        _context.Courses.Add(courses.addCourse());
                        await _context.SaveChangesAsync();
                        GeneralMsgDto SucMsg = new GeneralMsgDto(
                                    SuccessfullyMsgs.The_operation_was_completed_successfully,
                                        "Enter the requird filled",
                                          "Ther is not any data to Update it ");
                        return SucMsg;
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.The_Code_You_Are_Trying_To_Add_Has_Already_Been_Added,
                                  "Enter the requird filled",
                                  "Ther is not any data to Update it ");
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                     IErrorMsgs.The_Name_Of_The_Item_You_Are_Trying_To_Add_Has_Already_Been_Added,
                        "Enter the requird filled",
                        "Ther is not any data to Update it");
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public async Task<GeneralMsgDto> DeleteCourse(int id)
        {
            var course =await _context.Courses.FirstOrDefaultAsync(u => u.CouresId == id);
            try
            {
                if (course != null)
                {
                    _context.Courses.Remove(course);
                   await _context.SaveChangesAsync();
                    GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                  SuccessfullyMsgs.The_operation_was_completed_successfully,
                                                  "Enter the requird filled",
                                                  "Ther is not any data to Update it "
                                                );
                    return SucMsg;
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                  IErrorMsgs.Article_not_found,
                                                  "Enter the requird filled",
                                                  "Ther is not any data to Update it "
                                                  );
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public async Task<List<GetAllDataCourseQDto>> GetAllData()
        {
            try
            {
                string sql = @"	 
                            SELECT 
	                            c.CouresId,
	                            c.CourseName,
	                            c.CourseIevel,
	                            c.CourseCode,
	                            c.CourseAcademicHours,
                                c.WeeklyRatingPercentage,
	                            d.DepartmentId,
	                            d.DepartmentName
                                FROM Course c
                                JOIN Department d on c.DepartmentId = d.DepartmentId;
                        ";
                var result =await _context.Database.SqlQueryRaw<GetAllDataCourseQDto>(sql).ToListAsync();
                if (result == null)
                    return null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public async Task<List<GetAllDataCourseQDto>> GetCorseByDepartement(int DepartementId)
        {
            var departementId =await _context.Courses.FirstOrDefaultAsync(d => d.DepartmentId == DepartementId);
            try
            {
                if (departementId != null)
                {
                    string sql = @"	 
                                 SELECT
                              c.CouresId,
                              c.CourseIevel,
                              c.CourseName,
                              c.CourseCode,
                              c.CourseAcademicHours,
                              c.WeeklyRatingPercentage,
                              d.DepartmentId,
                              d.DepartmentName
                              FROM Department d
                              JOIN Course c ON d.DepartmentId = c.DepartmentId
                           WHERE d.DepartmentId=@p0;
                        ";
                    var result =await _context.Database.SqlQueryRaw<GetAllDataCourseQDto>(sql, DepartementId).ToListAsync();
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
        public async Task<List<GetCourseDto>> GetCourse()
        {
            try
            {
                var course =await _context.Courses
                  .Select(h => new GetCourseDto
                  {
                      CouresId = h.CouresId,
                      CourseIevel = h.CourseIevel,
                      DepartmentId = h.DepartmentId,
                      CourseName = h.CourseName,
                      CourseCode = h.CourseCode,
                      CourseAcademicHours = h.CourseAcademicHours,
                      WeeklyRatingPercentage = h.WeeklyRatingPercentage,
                  }).ToListAsync();
                return course;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public async Task<List<GetAllDataCourseQDto>> GetCourseByCourseLevel(int CourseLevel)
        {
            var courselevel =await _context.Courses.FirstOrDefaultAsync(d => d.CourseIevel == CourseLevel);
            try
            {
                if (courselevel != null)
                {
                    string sql = @"	 
                                      SELECT
                                      c.CouresId,
                                      c.CourseIevel,
                                      c.CourseName,
                                      c.CourseCode,
                                      c.CourseAcademicHours,
                                      c.WeeklyRatingPercentage,
                                      d.DepartmentId,
                                      d.DepartmentName
                                      FROM Department d
                                      JOIN Course c ON d.DepartmentId = c.DepartmentId
                                   WHERE c.CourseIevel=@p0;
                        ";
                    var result = await _context.Database.SqlQueryRaw<GetAllDataCourseQDto>(sql, CourseLevel).ToListAsync();
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
        public async Task<List<GetAllDataCourseQDto>> GetCourseByCourseId(int CourseId)
        {
            try
            {
                
                    string sql = @"	 
                                      SELECT
                                      c.CouresId,
                                      c.CourseIevel,
                                      c.CourseName,
                                      c.CourseCode,
                                      c.CourseAcademicHours,
                                      c.WeeklyRatingPercentage,
                                      d.DepartmentId,
                                      d.DepartmentName
                                      FROM Department d
                                      JOIN Course c ON d.DepartmentId = c.DepartmentId
                                   WHERE c.CouresId = @p0;
                        ";
                    var result = await _context.Database.SqlQueryRaw<GetAllDataCourseQDto>(sql, CourseId).ToListAsync();
                    if (result == null)
                        return null;
                    return result;
                
                
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public async Task<List<GetAllDataCourseQDto>> GetCourseByCourseLevelAndDepartement(int CourseLevel, int DepartementId)
        {
            try
            {
                string sql = @"	 
                                 SELECT
                                      c.CouresId,
                                      c.CourseIevel,
                                      c.CourseName,
                                      c.CourseCode,
                                      c.CourseAcademicHours,
                                      c.WeeklyRatingPercentage,
                                      d.DepartmentId,
                                      d.DepartmentName
                                      FROM Department d
                                      JOIN Course c ON d.DepartmentId = c.DepartmentId
                                   WHERE c.CourseIevel=@p0 and d.DepartmentId=@p1;
                        ";
                var result = await _context.Database.SqlQueryRaw<GetAllDataCourseQDto>(sql, CourseLevel, DepartementId).ToListAsync();
                if (result == null)
                    return null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public async Task<GeneralMsgDto> UpdateCourseDate(int id, UpdateCourseDto dto)
        {
            var CourseName = await _context.Courses
    .AnyAsync(c => c.CourseName == dto.CourseName && c.CouresId != id);
            var courses = await _context.Courses.FirstOrDefaultAsync(u => u.CouresId == id);

           try
            {
                bool isCourseDataSame =
                    courses.CourseIevel == dto.CourseIevel &&
                    courses.DepartmentId == dto.DepartmentId &&
                    courses.CourseName == dto.CourseName &&
                    courses.CourseAcademicHours == dto.CourseAcademicHours &&
                    courses.WeeklyRatingPercentage == dto.WeeklyRatingPercentage;
                if (courses != null)
                {
                    if (!isCourseDataSame)
                    {
                        if (CourseName == false)
                        {
                            courses.CourseIevel = dto.CourseIevel;
                            courses.CourseName = dto.CourseName;
                            courses.DepartmentId = dto.DepartmentId;
                            courses.CourseAcademicHours = dto.CourseAcademicHours;
                            courses.WeeklyRatingPercentage = dto.WeeklyRatingPercentage;
                            _context.Courses.Update(courses);
                            await _context.SaveChangesAsync();
                            GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                SuccessfullyMsgs.The_operation_was_completed_successfully,
                                                "Enter the requird filled",
                                                "Ther is not any data to Update it "
                                              );
                            return SucMsg;
                        }
                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.The_Name_Of_The_Item_You_Are_Trying_To_Add_Has_Already_Been_Added,
                                "Enter the requird filled",
                                "Ther is not any data to Update it "
                             );
                            return ErrorMsg;
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.You_Have_Oot_Made_Any_Modification_Please_Make_Any_Modifications_For_The_Modification_Process_To_Be_Successful,
                            "Enter the requird filled",
                            "Ther is not any data to Update it "
                         );
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                         IErrorMsgs.Article_not_found,
                                         "Enter the requird filled",
                                         "Ther is not any data to Update it "
                                       );
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
    }
}

