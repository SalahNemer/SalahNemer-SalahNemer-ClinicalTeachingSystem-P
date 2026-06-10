using DataBase.DBcon;
using DevetionStudetns.DTO;
using DevetionStudetns.DTO.Hospital;
using DevetionStudetns.Service;
using FinalProject.DTO.CourseDTO;
using FinalProject.Service;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using V1.DTO.CourseDTO;

namespace DevetionStudetns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCountroller : ControllerBase
    {
        private readonly CourseServes _courseServes;
        private readonly DBC _db;
        public CourseCountroller(CourseServes courseServes,DBC db)
        {
            _courseServes = courseServes;
            _db = db;
        }

        [HttpGet("get all courses")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _courseServes.getCourse());
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }
        [HttpGet("GetAllDataCourse")]
        public async Task<IActionResult> GetAllData()
        {
            try
            {
                return Ok(await _courseServes.GetAllData());
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GetByDepartement")]
        public async Task<IActionResult> GetCourseByDepartement(int departementId)
        {
            var departementID =await _db.Courses.FirstOrDefaultAsync(id => id.DepartmentId == departementId);
            var Departement =await _db.Departments.FirstOrDefaultAsync(d => d.DepartmentId == departementId);
            try
            {
                if (Departement != null)
                {
                    if (departementID != null)
                    {
                        return Ok(await _courseServes.GetCorseByDepartementId(departementId));
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.The_Entered_Section_Does_Not_Contain_Any_Materials,
                            "Enter the requird filled",
                            "there is not any data"
                            );
                        return BadRequest(ErrorMsg);
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                        IErrorMsgs.The_Entered_Partition_Is_Not_Included_In_The_Partition_List,
                        "Enter the requird filled",
                        "there is not any data"
                        );
                    return BadRequest(ErrorMsg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetCourseByCourseLevel")]
        public async Task<IActionResult> GetCourseByCourseLevel(int CourseLevel)
        {
            var courselevel =await _db.Courses.FirstOrDefaultAsync(d => d.CourseIevel == CourseLevel);
            try
            {
                if (courselevel != null)
                {
                    return Ok(await _courseServes.getCourseByCourseLevel(CourseLevel));
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                        IErrorMsgs.Course_Level_Not_Found,
                        "Enter the requird filled",
                        "there is not any data"
                        );
                    return BadRequest(ErrorMsg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpGet("GetCourseByCourseLevelAndDepartement")]
        public async Task<IActionResult> GetCourseByCourseLevelAndDepartement(int CourseLevel,int departement)
        {
            var courselecel = await _db.Courses.FirstOrDefaultAsync(cl => cl.CourseIevel == CourseLevel);
            var Departement = await _db.Departments.FirstOrDefaultAsync(d => d.DepartmentId == departement);
            var CourseByDepartement = await _db.Courses.FirstOrDefaultAsync(dep => dep.DepartmentId == departement);
            var Course=await _db.Courses.FirstOrDefaultAsync(c=>c.DepartmentId== departement && c.CourseIevel== CourseLevel);
            try
            {
                if (courselecel != null)
                {
                    if (Departement != null)
                    {
                        if (CourseByDepartement != null)
                        {
                            if (Course != null)
                            {
                                return Ok(await _courseServes.getCourseByCourseLevelAndDepartement(CourseLevel, departement));
                            }
                            else
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.The_Entrance_Section_Does_Not_Contain_Materials_At_This_Level,
                                    "Enter the requird filled",
                                    "there is not any data"
                                    );
                                return BadRequest(ErrorMsg);
                            }
                        }
                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.The_Entered_Section_Does_Not_Contain_Any_Materials,
                                "Enter the requird filled",
                                "there is not any data"
                                );
                            return BadRequest(ErrorMsg);
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.The_Entered_Partition_Is_Not_Included_In_The_Partition_List,
                            "Enter the requird filled",
                            "there is not any data"
                            );
                        return BadRequest(ErrorMsg);
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                        IErrorMsgs.There_Are_No_Materials_In_This_Level,
                        "Enter the requird filled",
                        "there is not any data"
                        );
                    return BadRequest(ErrorMsg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        [HttpPost("add corse")]
        public async Task<IActionResult> add(CourseDto courseDto)
        {
            try
            {
                var result = await _courseServes.AddCourse(courseDto);
                if (result.ErrorMsg == "تمت العملية بنجاح")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpDelete("delete course")]
        public async Task<IActionResult> delete(int CourseId)
        {
            try
            {
                return Ok(await _courseServes.deleteCourse(CourseId));
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpPut("update course")]
        public async Task<IActionResult> Update(int CourseId, [FromBody] UpdateCourseDto courseDto)
        {
            try
            {
                var result = await _courseServes.updateCourse(CourseId, courseDto);
                if (result.ErrorMsg == "تمت العملية بنجاح")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

        [HttpGet("GitCourseById/{CourseId}")]
        public async Task<IActionResult> GetCourseByCourseId(int CourseId)
        {
            return  Ok(await _courseServes.GetCourseByCourseId(CourseId));
        }

    }
}
