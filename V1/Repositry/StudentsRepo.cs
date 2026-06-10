using DataBase.DBcon;
using DevetionStudetns.DTO.DivisionsDTO;
using DevetionStudetns.DTO.StudentsDTO;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Mappers.StudentsMapper;
using DevetionStudetns.NewFolder;
using FinalProject.DTO.StudentsDTO;
using FinalProject.Interface.IRepositry;
using FinalProject.Mappers.StudentsMapper;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using V1.DTO.StudentsDTO;

namespace DevetionStudetns.Repositry.StudentsReposetry
{
    public class StudentsRepo: IStudents
    {
        private readonly DBC _context;
        public StudentsRepo(DBC context)
        {
            _context = context;
        }

        public List<GetStudentsQDto> getStudentsData(string userId)
        {
            string sql = @"	 
                         SELECT 
                            u.UserId,
                            u.FullName,
                            u.IdNumber,
                            u.Email,
                            u.Gender,
                            u.PhoneNumber,
                            s.StudentLevel,
                            s.CumulativeAverage,
	                        s.YearEnrollment  ,
	                        u.Address 

                        FROM 
                            Users u
                        JOIN 
                            Students s ON s.UserId = u.UserId
                        where 
                        u.UserId = @userId
                           ";

            var result = _context.Database.SqlQueryRaw<GetStudentsQDto>(
                sql, new SqlParameter("userId", userId)).ToList();
            if (result != null )
                return result;
            return null;
        }
        public List<GetStudentsQ2Dto> GetAllStudentsInOneSubGroupById(int SubGroupId)
        {
            string sql = @"SELECT 
							a.DivisionId,
                            u.UserId,
                            u.FullName,
                            u.IdNumber,
                            u.Email,
                            u.Gender,
                            u.PhoneNumber,
                            s.StudentLevel,
                            s.CumulativeAverage    
                        FROM 
                            Users u
                        JOIN 
                            Students s ON s.UserId = u.UserId
                        JOIN 
                            Divisions g ON g.StudentId = u.UserId
							join Divisions a on a.StudentId = s.UserId 
                        WHERE 
                            g.SubGroupId = @SubGroupId
                                                ";

            var result = _context.Database.SqlQueryRaw<GetStudentsQ2Dto>(
                sql, new SqlParameter("SubGroupId", SubGroupId)).ToList();
            if (result == null)
                return null;
            return result;
        }
        public List<GetStudentsQ1Dto> GetAllStudentsInSameLevelInAllSubGroup(int Level)
        {
            string sql = @"
                            SELECT 
                                u.UserId,
                                u.FullName,
                                u.IdNumber,
                                u.Email,
                                u.PhoneNumber,
                                s.StudentLevel, 
                                s.CumulativeAverage,
	                            g.SubGroupSympole,
	                            s.YearEnrollment,
	                            u.Address 
                            FROM 
                                Students s
                            JOIN Users u ON u.UserId = s.UserId 
                            LEFT JOIN Divisions d ON u.UserId = d.StudentId
                            LEFT JOIN SubGroup g ON g.SubGroupId = d.SubGroupId
                            WHERE 
                                u.RoulName = 7 
                                AND s.StudentLevel = @Level
                             	and u.AccountStatus = 1

                                                ";

            var result = _context.Database.SqlQueryRaw<GetStudentsQ1Dto>(
                sql, new SqlParameter("Level", Level)).ToList();
            if (result == null)
                return null;
            return result;
        }
        public List<GetStudentsQ1Dto> GetAllStudents()
        {
            string sql = @"
                            SELECT 
                                u.UserId,
                                u.FullName,
                                u.IdNumber,
                                u.Email,
                                u.PhoneNumber,
                                s.StudentLevel, 
                                s.CumulativeAverage,
	                            g.SubGroupSympole,
                            	s.YearEnrollment,
	                            u.Address 

                            FROM 
                                Students s
                            JOIN Users u ON u.UserId = s.UserId 
                            LEFT JOIN Divisions d ON u.UserId = d.StudentId
                            LEFT JOIN SubGroup g ON g.SubGroupId = d.SubGroupId
                                                ";

            var result = _context.Database.SqlQueryRaw<GetStudentsQ1Dto>(
                sql).ToList();
            if (result == null)
                return null;
            return result;
        }

        public List<GetStudentsQDto> GetAllStudentsInSameLevel(int Level)
        {
            string sql = @"
                              SELECT 
                                    u.UserId,
                                    u.FullName,
                                    u.IdNumber,
                                    u.Email,
                                    u.PhoneNumber,
                                    s.StudentLevel, 
                                    s.CumulativeAverage,
	                                g.SubGroupSympole,
                                    s.YearEnrollment,
	                                u.Address 
                                FROM 
                                    Students s
                                JOIN Users u ON u.UserId = s.UserId 
                                LEFT JOIN Divisions d ON u.UserId = d.StudentId
                                LEFT JOIN SubGroup g ON g.SubGroupId = d.SubGroupId
                                WHERE 
                                    u.RoulName = 7 
                                    AND s.StudentLevel = @Level
                                    AND g.SubGroupId IS NULL
	                                and u.AccountStatus = 1

                                                ";

            var result = _context.Database.SqlQueryRaw<GetStudentsQDto>(
                sql, new SqlParameter("Level", Level)).ToList();
            if (result == null)
                return null;
            return result;
        }

        public GeneralMsgDto AddStudentds(StudentsAddDto studentsAddDto)
        {
            if (studentsAddDto == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                             IErrorMsgs.INVALID_DATA_FORMAT,
                                             "Enter the Courect Data ",
                                             "Enter Courect Data for Add Division "
                                             );
                return ErrorMsg;
            }

            else
            {
                var getStudents = _context.students.Where(p => p.UserId == studentsAddDto.UserId).ToList().Count;
                if (getStudents != 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.DUPLICATE_RECORD_RERRO,
                                        "Entering duplicate data",
                                        "Enter Data with no ant duplicate "
                                        );
                    return ErrorMsg;
                }
                else
                {
                    var getUser = _context.Users.Where(p => p.UserId == studentsAddDto.UserId).ToList().Count;
                    if (getUser == 0)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.STUDENTS_NOT_FOUND_IN_USER_TABLE,
                                    "Not found",
                                    "There is not any user have this id "
                                    );
                        return ErrorMsg;
                    }
                    var getUserRole = _context.Users.Where(p => p.UserId == studentsAddDto.UserId && p.RoleId == 7).ToList().Count;
                    if (getUserRole == 0)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.Students_Role,
                                    "Not found",
                                    "Role Error "
                                    );
                        return ErrorMsg;
                    }
                    if (studentsAddDto.StudentLevel == 4 || studentsAddDto.StudentLevel == 5 || studentsAddDto.StudentLevel == 6)
                    {
                        _context.students.Add(studentsAddDto.AddStudntsMapper());
                        _context.SaveChanges();
                        _context.SaveChanges();
                        GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                              SuccessfullyMsgs.STUDENT_REGISTERED_SUCCESSFULLY,
                              "Add Successfully",
                              "You Are Add New Students "
                              );
                        return SuccessfullyMsg;
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.DUPLICATE_RECORD_RERRO,
                                        "Entering duplicate data",
                                        "Enter Students level between 4 to 6  "
                                        );
                        return ErrorMsg;
                    }
                }
            }
        }

        public GeneralMsgDto DeleteStudents(string StudentsId)
        {
            if (StudentsId == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.INVALID_DATA_FORMAT,
                "Enter the requird filled",
                           Convert.ToString(StudentsId)
                           );
                return ErrorMsg;
            }
            else
            {
                var  DeleteStudents = _context.students.FirstOrDefault(p => p.UserId == StudentsId);
                if (DeleteStudents != null)
                {
                    _context.students.Remove(DeleteStudents);
                    _context.SaveChanges();
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                   SuccessfullyMsgs.SUCCESSFUL_DELETE,
                                                   "Successfully Delete",
                                                   "You are delete Students "
                                                   );
                    return ErrorMsg;
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.NOT_FOUND_DATA,
                           "Enter the requird filled",
                           "Ther is not any data to delete it "
                           );
                    return ErrorMsg;
                }
            }

        }

        public GeneralMsgDto UpdateStudents(UpdateStudentsDto NewStudents , string studentsId )
        {
            if (NewStudents == null || studentsId == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                             IErrorMsgs.INVALID_DATA_FORMAT,
                                             "Enter the Courect Data ",
                                             "Enter Courect data Students  "
                                             );
                return ErrorMsg;
            }

            else
            {
                var oldStudentsData = _context.students.FirstOrDefault(p => p.UserId == studentsId);
                if (oldStudentsData != null)
                {
                    if (NewStudents.StudentLevel == 4 || NewStudents.StudentLevel == 5 || NewStudents.StudentLevel == 6)
                    {
                      
                        oldStudentsData.StudentLevel = NewStudents.StudentLevel;
                        oldStudentsData.CumulativeAverage = NewStudents.CumulativeAverage;
                        oldStudentsData.YearEnrollment = Convert.ToString(NewStudents.YearEnrollment);

                        _context.SaveChanges();

                        GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                              SuccessfullyMsgs.UPDATE_SUCCESSFUL,
                              "Update Successfully",
                              "You Are update Student data  "
                              );
                        return SuccessfullyMsg;
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.DUPLICATE_RECORD_RERRO,
                                        "Entering duplicate data",
                                        "Enter Students level between 4 to 6  "
                                        );
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                       IErrorMsgs.DOCTOR_NOT_FOUND,
                                       "Fialed",
                                       "There is not any Studnets have smae this id  : " + studentsId
                                       );
                    return ErrorMsg;
                }
            }
        }
        public async Task<GeneralMsgDto> UpdateStudentAndUser(UpdateStudentAndUserDto students, string studentsId)
        {
            var student = await _context.students.FirstOrDefaultAsync(s => s.UserId == studentsId);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == students.UserId);

            if (student == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                   IErrorMsgs.STUDENT_ID_NOT_FOUND_IN_STUDENTS_TABLE,
                   "Fialed",
                   "There is not any Studnets have smae this id  : " + studentsId
                   );
                return ErrorMsg;
            }
            else
            {
                bool isStudentDataSame =
                    student.StudentLevel == students.StudentLevel &&
                    student.CumulativeAverage == students.CumulativeAverage &&
                    student.YearEnrollment == Convert.ToString(students.YearEnrollment);

                bool isUserDataSame =
                    user.FirstName == students.FirstName &&
                    user.LastName == students.LastName &&
                    user.Email == students.Email &&
                    user.Address == students.Address &&
                    user.Password == students.Password &&
                    user.DateOfBarth == students.DateOfBarth &&
                    user.Gender == students.Gender &&
                    user.PhoneNumber == students.PhoneNumber &&
                    user.RoleId == students.RoleId &&
                    user.AccountStatus == students.AccountStatus &&
                    user.IdNumber == students.IdNumber &&
                    user.FullName == students.FirstName + " " + students.LastName;

                if (isStudentDataSame && isUserDataSame)
                {
                    return new GeneralMsgDto(
                            IErrorMsgs.You_Have_Oot_Made_Any_Modification_Please_Make_Any_Modifications_For_The_Modification_Process_To_Be_Successful,
                            "Failed",
                            "No changes were detected, update not required."
                        );
                }
                else
                {
                    student.StudentLevel = students.StudentLevel;
                    student.CumulativeAverage = students.CumulativeAverage;
                    student.YearEnrollment = Convert.ToString(students.YearEnrollment);

                    user.FirstName = students.FirstName;
                    user.LastName = students.LastName;
                    user.Email = students.Email;
                    user.Address = students.Address;
                    user.Password = students.Password;
                    user.DateOfBarth = students.DateOfBarth;
                    user.Gender = students.Gender;
                    user.PhoneNumber = students.PhoneNumber;
                    user.RoleId = students.RoleId;
                    user.AccountStatus = students.AccountStatus;
                    user.IdNumber = students.IdNumber;
                    user.FullName = students.FirstName + " " + students.LastName;

                    _context.Update(student);
                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    return new GeneralMsgDto(
                        SuccessfullyMsgs.UPDATE_SUCCESSFUL,
                        "Update Successfully",
                        "You have updated student data successfully."
                    );
                }
            }
        }
    }
}
