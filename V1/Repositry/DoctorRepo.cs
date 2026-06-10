using database.models;
using DataBase.DBcon;
using DevetionStudetns.DTO.DistributionsMainGroupDTO;
using DevetionStudetns.DTO.DoctorDto;
using DevetionStudetns.DTO.Hospital;
using DevetionStudetns.DTO.MainGroupDTO;
using DevetionStudetns.DTO.UserDTO;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Interface;
using DevetionStudetns.Mappers.DoctorMappier;
using DevetionStudetns.NewFolder;
using FinalProject.DTO.DoctorDto;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using testDtoAndmapper.Entity;
using V1.DTO.DoctorDTO;

namespace DevetionStudetns.Repositry.DoctorReposotry
{
    public class DoctorRepo : IDoctor
    {
        private readonly DBC _db;
        public DoctorRepo(DBC db)
        {
            _db = db;
        }
        public async Task<GeneralMsgDto> Add(DoctorDto doctor)
        {
            var doctorss =await _db.doctors.FirstOrDefaultAsync(d => d.UserId == doctor.DoctorId && d.HospitalId == doctor.HospitalId && d.DepartmentID == doctor.DepartmentID &&d.MedicalSpecialty == doctor.MedicalSpecialty);
            var user =await _db.Users.FirstOrDefaultAsync(u => u.UserId == doctor.DoctorId);
            var hospital =await _db.hospitals.FirstOrDefaultAsync(h => h.HospitalId == doctor.HospitalId);
            var departement =await _db.Departments.FirstOrDefaultAsync(de => de.DepartmentId == doctor.DepartmentID);

            try
            {
                if (user != null )
                {
                    if (hospital != null)
                    {
                        if (departement != null)
                        {
                            if (doctorss == null)
                            {
                                _db.doctors.Add(doctor.Doctor());
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
                                GeneralMsgDto ErrorMsgs = new GeneralMsgDto(
                                        IErrorMsgs.The_doctor_is_already_there,
                                        "Enter the requird filled",
                                        "Ther is not any data to Update it "
                                      );
                                return ErrorMsgs;
                            }
                        }
                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                            IErrorMsgs.The_section_does_not_exist,
                                            "Enter the requird filled",
                                            "Ther is not any data to Update it "
                                          );
                            return ErrorMsg;
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                          IErrorMsgs.The_hospital_does_not_exist,
                          "Enter the requird filled",
                          "Ther is not any data to Update it "
                        );
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsgs = new GeneralMsgDto(
                                                    IErrorMsgs.The_Dctor_You_Are_Trying_To_Add_Does_Not_Exist_In_The_Users_Table,
                                                    "Enter the requird filled",
                                                    "Ther is not any data to Update it "
                                                  );
                    return ErrorMsgs;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public async Task<GeneralMsgDto> Delete(string id)
        {
            var doctor =await _db.doctors.FirstOrDefaultAsync(u => u.UserId == id);
            try
            {
                if (doctor != null)
                {
                    _db.doctors.Remove(doctor);
                    await _db.SaveChangesAsync();
                    GeneralMsgDto SucMsg = new GeneralMsgDto(
                                             SuccessfullyMsgs.The_deletion_was_completed_successfully,
                                             "Success",
                                             "Success"
                                           );
                    return SucMsg;
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                           IErrorMsgs.The_doctor_is_not_present,
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

        public async Task<List<DoctorDto>> Get()
        {
            List<Doctor> doctors=await _db.doctors.ToListAsync();
            List<DoctorDto> doctorsDto= new List<DoctorDto>();
            try
            {
                if (doctors != null)
                {
                    foreach (Doctor doctor in doctors)
                    {
                        doctorsDto.Add(doctor.GetDoc());
                    }
                    return doctorsDto;
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

        public async Task<List<GetAllDataQDto>> GetDoctorsByDepartement(int departementID)
        {
            var DepartementId=await _db.doctors.FirstOrDefaultAsync(d=>d.DepartmentID==departementID);
            try
            {
                if (DepartementId != null)
                {
                    string sql = @"	 
                       SELECT
                          d.UserId,
                          u.FirstName,
                          u.LastName,
                          u.FullName,
                	      u.RoulName,
                          d.MedicalSpecialty,
                          d.AcademicDegree,
                          d.YearOfObtainingTheCertificate,
                          d.YearsExperience,
                          d.TheUniversityFromWhichHeObtainedHisLastDegree,
                          d.TheCountryYouGraduatedFrom,
                          h.HospitalId,
                          h.HospitalName,
                          dep.DepartmentId,
                          dep.DepartmentName
                          FROM Users u
                          JOIN Doctors d ON u.UserId = d.UserId
                          JOIN Hospitals h ON d.HospitalId = h.HospitalId
                          JOIN Department dep ON d.DepartmentId = dep.DepartmentId
	                      where dep.DepartmentId=@p0;
                        ";
                    var result =await _db.Database.SqlQueryRaw<GetAllDataQDto>(sql, departementID).ToListAsync();
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

        public async Task<List<GetAllDataQDto>> Get_All_Data()
        {
            try
            {
                string sql = @"	 
                       SELECT
                            d.UserId,
                            u.FirstName,
                            u.LastName,
                            u.FullName,
	                        u.RoulName,
                            d.MedicalSpecialty,
                            d.AcademicDegree,
                            d.YearOfObtainingTheCertificate,
                            d.YearsExperience,
                            d.TheUniversityFromWhichHeObtainedHisLastDegree,
                            d.TheCountryYouGraduatedFrom,
                            h.HospitalId,
                            h.HospitalName,
                            dep.DepartmentId,
                            dep.DepartmentName
                            FROM Users u
                            JOIN Doctors d ON u.UserId = d.UserId
                            JOIN Hospitals h ON d.HospitalId = h.HospitalId
                            JOIN Department dep ON d.DepartmentId = dep.DepartmentId;
                        ";
                var result =await _db.Database.SqlQueryRaw<GetAllDataQDto>(sql).ToListAsync();
                if (result == null)
                    return null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public async Task<GeneralMsgDto> Update(string id, UpdateDoctorDto doctors)
        {
            var doctor =await _db.doctors.FirstOrDefaultAsync(u => u.UserId == id);
            var departementD =await _db.doctors.FirstOrDefaultAsync(dd => dd.DepartmentID == doctors.DepartmentID);
            var hospitalD =await _db.doctors.FirstOrDefaultAsync(hd => hd.HospitalId == doctors.HospitalId);
            var medicalSpecialty=await _db.doctors.FirstOrDefaultAsync(m=>m.MedicalSpecialty == doctors.MedicalSpecialty);
            var hospital =await _db.hospitals.FirstOrDefaultAsync(h => h.HospitalId == doctors.HospitalId);
            var departement=await _db.Departments.FirstOrDefaultAsync(d=>d.DepartmentId==doctors.DepartmentID);
            try
            {
                if (doctor != null)
                {
                    if (hospital != null)
                        {
                            if (departement != null)
                            {
                                doctor.HospitalId = doctors.HospitalId;
                                doctor.DepartmentID = doctors.DepartmentID;
                                doctor.MedicalSpecialty = doctors.MedicalSpecialty;
                                doctor.AcademicDegree = doctors.AcademicDegree;
                                doctor.YearOfObtainingTheCertificate = doctors.YearOfObtainingTheCertificate;
                                doctor.YearsExperience = doctors.YearsExperience;
                                doctor.TheUniversityFromWhichHeObtainedHisLastDegree = doctors.TheUniversityFromWhichHeObtainedHisLastDegree;
                                doctor.TheCountryYouGraduatedFrom = doctors.TheCountryYouGraduatedFrom;

                                _db.doctors.Update(doctor);
                               await _db.SaveChangesAsync();
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
                                                IErrorMsgs.The_section_does_not_exist,
                                                "Enter the requird filled",
                                                "Ther is not any data to Update it "
                                              );
                                return ErrorMsg;
                            }
                        }
                        else
                        {
                            if (departement != null)
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                            IErrorMsgs.The_hospital_does_not_exist,
                                            "Enter the requird filled",
                                            "Ther is not any data to Update it "
                                          );
                                return ErrorMsg;
                            }
                            else
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                           IErrorMsgs.The_hospital_does_not_exist_and_The_section_does_not_exist,
                                           "Enter the requird filled",
                                           "Ther is not any data to Update it "
                                         );
                                return ErrorMsg;
                            }
                        }
                }
                else
                {

                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                         IErrorMsgs.The_doctor_is_not_present,
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
        public List<GetDoctorQ1Dto> GetDoctorById (string DcotroId )
        {
           
                string sql = @"	 
                      select 
                       a.UserId ,
                       a.HospitalId,
                       a.DepartmentId,
                       a.MedicalSpecialty ,
                       a.YearOfObtainingTheCertificate,
                       a.AcademicDegree,
                       a.TheCountryYouGraduatedFrom,
                       a.TheUniversityFromWhichHeObtainedHisLastDegree,
                       a.YearsExperience
                       from Doctors a 
                       where a.UserId = @p0
                        ";
                var result =  _db.Database.SqlQueryRaw<GetDoctorQ1Dto>(sql , DcotroId).ToList();
            if (result == null)
                return null;
             return result;
          
        }
        public async Task<GeneralMsgDto> UpdateDoctorAndUser(UpdateDoctorAndUserDto Doctor, string DoctorId)
        {
            var DOCTOR = await _db.doctors.FirstOrDefaultAsync(d => d.UserId == DoctorId);
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == Doctor.DoctorId);

            if (DOCTOR == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                   IErrorMsgs.DOCTOR_ID_NOT_FOUND,
                   "Fialed",
                   "There is not any Studnets have smae this id  : " + DoctorId
                   );
                return ErrorMsg;
            }
            else
            {
                bool isDoctorDataSame =
                    DOCTOR.UserId == Doctor.DoctorId &&
                    DOCTOR.HospitalId == Doctor.HospitalId &&
                    DOCTOR.DepartmentID == Doctor.DepartmentID &&
                    DOCTOR.MedicalSpecialty == Doctor.MedicalSpecialty &&
                    DOCTOR.AcademicDegree == Doctor.AcademicDegree &&
                    DOCTOR.YearOfObtainingTheCertificate == Doctor.YearOfObtainingTheCertificate &&
                    DOCTOR.YearsExperience == Doctor.YearsExperience &&
                    DOCTOR.TheUniversityFromWhichHeObtainedHisLastDegree == Doctor.TheUniversityFromWhichHeObtainedHisLastDegree &&
                    DOCTOR.TheCountryYouGraduatedFrom == Doctor.TheCountryYouGraduatedFrom;

                bool isUserDataSame =
                    user.FirstName == Doctor.FirstName &&
                    user.LastName == Doctor.LastName &&
                    user.Email == Doctor.Email &&
                    user.Address == Doctor.Address &&
                    user.Password == Doctor.Password &&
                    user.DateOfBarth == Doctor.DateOfBarth &&
                    user.Gender == Doctor.Gender &&
                    user.PhoneNumber == Doctor.PhoneNumber &&
                    user.RoleId == Doctor.RoleId &&
                    user.AccountStatus == Doctor.AccountStatus &&
                    user.IdNumber == Doctor.IdNumber &&
                    user.FullName == Doctor.FirstName + " " + Doctor.LastName;

                if (isDoctorDataSame && isUserDataSame)
                {
                    return new GeneralMsgDto(
                            IErrorMsgs.You_Have_Oot_Made_Any_Modification_Please_Make_Any_Modifications_For_The_Modification_Process_To_Be_Successful,
                            "Failed",
                            "No changes were detected, update not required."
                        );
                }
                else
                {
                    DOCTOR.UserId = Doctor.DoctorId;
                    DOCTOR.HospitalId = Doctor.HospitalId;
                    DOCTOR.DepartmentID = Doctor.DepartmentID;
                    DOCTOR.MedicalSpecialty = Doctor.MedicalSpecialty;
                    DOCTOR.AcademicDegree = Doctor.AcademicDegree;
                    DOCTOR.YearOfObtainingTheCertificate = Doctor.YearOfObtainingTheCertificate;
                    DOCTOR.YearsExperience = Doctor.YearsExperience;
                    DOCTOR.TheUniversityFromWhichHeObtainedHisLastDegree = Doctor.TheUniversityFromWhichHeObtainedHisLastDegree;
                    DOCTOR.TheCountryYouGraduatedFrom = Doctor.TheCountryYouGraduatedFrom;

                    user.FirstName = Doctor.FirstName;
                    user.LastName = Doctor.LastName;
                    user.Email = Doctor.Email;
                    user.Address = Doctor.Address;
                    user.Password = Doctor.Password;
                    user.DateOfBarth = Doctor.DateOfBarth;
                    user.Gender = Doctor.Gender;
                    user.PhoneNumber = Doctor.PhoneNumber;
                    user.RoleId = Doctor.RoleId;
                    user.AccountStatus = Doctor.AccountStatus;
                    user.IdNumber = Doctor.IdNumber;
                    user.FullName = Doctor.FirstName + " " + Doctor.LastName;

                    _db.Update(DOCTOR);
                    _db.Update(user);
                    await _db.SaveChangesAsync();

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
