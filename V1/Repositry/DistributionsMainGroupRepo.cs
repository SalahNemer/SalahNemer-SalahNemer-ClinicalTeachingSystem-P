using DataBase.DBcon;
using DevetionStudetns.DTO.AppointmentsDTO;
using DevetionStudetns.DTO.DistributionsMainGroupDTO;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Mappers.AddDistributionsMainGroupMapper;
using DevetionStudetns.NewFolder;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using V1.DTO.DistributionMainGroupDTO;

namespace DevetionStudetns.Repositry.DistributionsMainGroupRepositry
{
    public class DistributionsMainGroupRepo : IDistributionsMainGroup
    {
        private readonly DBC _context;
        public DistributionsMainGroupRepo ( DBC context)
        {
            _context = context;
        }

        public GeneralMsgDto AddDistributionsMainGroup(AddDistributionsMainGroupDto addDistributionsMainGroupDto)
        {
            try
            {
                if (addDistributionsMainGroupDto == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.MUST_FILL_ALL_FILLED,
                                "Enter the requird filled",
                                "there is not any data"
                                );
                    return ErrorMsg;
                }
                else
                {
                    var CountManyOfDistrbutionOneMainGroup = _context.DistributionsMainGroup.Where(p => p.MainGroupId == addDistributionsMainGroupDto.MainGroupId).ToList().Count;
                    var ValedationDistrbutionOneMainGroupInTheDapatments = _context.DistributionsMainGroup.Where(p => p.MainGroupId == addDistributionsMainGroupDto.MainGroupId && p.DepartmentId == addDistributionsMainGroupDto.DepartmentId).ToList().Count;

                    if (CountManyOfDistrbutionOneMainGroup >= 3)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.FAILED_GRATE_LEMET,
                                              "Enter the Courect data",
                                              "You exceeded the limit in distributing the main group over the periods, as the limit is distributing the group over 3 periods"
                                              );
                        return ErrorMsg;
                    }
                    if (ValedationDistrbutionOneMainGroupInTheDapatments >= 1)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                             IErrorMsgs.FAILED_distributed_to_more_than_one_department,
                                             "Enter the Courect  data ",
                                             "The main group must be distributed to more than one department during its shift in 3 periods"
                                             );
                        return ErrorMsg;
                    }

                    else
                    {
                        var getMainGroupById = _context.mainGrops.Where(p => p.MainGroupId == addDistributionsMainGroupDto.MainGroupId).ToList().Count;
                        var getDepartmentById = _context.Departments.Where(p=>p.DepartmentId == addDistributionsMainGroupDto.DepartmentId).ToList().Count;
                        var getRotationById = _context.Rotations.Where(p=>p.RotationId == addDistributionsMainGroupDto.RotationId).ToList().Count;
                        int ValedationInput = getMainGroupById + getDepartmentById + getRotationById;
                        if (ValedationInput == 0)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                  IErrorMsgs.NOT_FOUND_DEPARTMENT + " , " + IErrorMsgs.NOT_FOUND_ROATATION + " , " + IErrorMsgs.NOT_FOUND_MAINGROUP,
                                                  "Error",
                                                  "Enter the corect data  "
                                                  );
                            return ErrorMsg;
                        }
                        else
                        {
                            if (getMainGroupById == 0)
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                  IErrorMsgs.NOT_FOUND_MAINGROUP,
                                                  "Error",
                                                  "Enter the corect data . for the main group "
                                                  );
                                return ErrorMsg;
                            }
                            if (getDepartmentById == 0)
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                  IErrorMsgs.NOT_FOUND_DEPARTMENT,
                                                  "Error",
                                                  "Enter the corect data . for the departments "
                                                  );
                                return ErrorMsg;
                            }
                            if (getRotationById == 0)
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                  IErrorMsgs.NOT_FOUND_ROATATION,
                                                  "Error",
                                                  "Enter the corect data . for the Rotation "
                                                  );
                                return ErrorMsg;
                            }
                            else
                            {

                                try
                                {
                                    _context.DistributionsMainGroup.Add(addDistributionsMainGroupDto.AddDistributionsMainGroup());
                                    _context.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                          IErrorMsgs.INVALID_DATA_FORMAT,
                                                          "Enter the Courect sympole of the Main Group",
                                                          "Error in data entry. The data entered is not included "
                                                          );
                                    return ErrorMsg;
                                }
                                GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                                 SuccessfullyMsgs.WORKSHOP_REGISTERED_SUCCESSFULLY,
                                                 "Add Successfully",
                                                 "You Are Add New Distributions Main Group "
                                                 );
                                return SuccessfullyMsg;

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public GeneralMsgDto DeleteDistributionsMainGroup(int DistributionsMainGroupId)
        {
            try
            {
                if (DistributionsMainGroupId == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                               IErrorMsgs.INVALID_DATA_FORMAT,
                               "Enter the requird filled",
                               Convert.ToString(DistributionsMainGroupId)
                               );
                    return ErrorMsg;
                }
                else
                {
                    var GetDistributionsMainGroupIdForDelete = _context.DistributionsMainGroup.FirstOrDefault(p => p.DistributionsMainGroupId == DistributionsMainGroupId);
                    if (GetDistributionsMainGroupIdForDelete != null)
                    {
                        _context.DistributionsMainGroup.Remove(GetDistributionsMainGroupIdForDelete);
                        _context.SaveChanges();
                        GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                       SuccessfullyMsgs.SUCCESSFUL_DELETE,
                                                       "Successfully Delete",
                                                       "You are delete this Distributions Main Group "
                                                       );
                        return SucMsg;
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
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public GeneralMsgDto UpdateDistributionsMainGroup(UpdateDistributionsMainGroupDto NewDistributionsMainGroupDto, int DistributionsMainGroupId)
        {
            try
            {
                if (NewDistributionsMainGroupDto == null || DistributionsMainGroupId == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                           IErrorMsgs.INVALID_DATA_FORMAT,
                                                           "Enter the requird filled",
                                                           Convert.ToString(DistributionsMainGroupId)
                                                           );
                    return ErrorMsg;
                }


                else
                {
                    var OldDistributionsMainGroupData = _context.DistributionsMainGroup.FirstOrDefault(p => p.DistributionsMainGroupId == DistributionsMainGroupId);

                    if (OldDistributionsMainGroupData == null)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                IErrorMsgs.NOT_FOUND_DATA,
                                                                "Enter the requird filled",
                                                                "Ther is not any data to update it "
                                                                );
                        return ErrorMsg;
                    }
                    else
                    {
                        var getDepartmentById = _context.Departments.Where(p => p.DepartmentId == NewDistributionsMainGroupDto.DepartmentId).ToList().Count;
                        if (getDepartmentById == 0)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                IErrorMsgs.NOT_FOUND_DEPARTMENT,
                                                "Error",
                                                "Enter the corect data . for the departments "
                                                );
                            return ErrorMsg;
                        }
                        
                        else
                        {

                        var CountManyOfDistrbutionOneMainGroup = _context.DistributionsMainGroup.Where(p => p.MainGroupId == OldDistributionsMainGroupData.MainGroupId).ToList().Count;
                        var ValedationDistrbutionOneMainGroupInTheDapatments = _context.DistributionsMainGroup.Where(p => p.MainGroupId == OldDistributionsMainGroupData.MainGroupId && p.DepartmentId == NewDistributionsMainGroupDto.DepartmentId).ToList().Count;
                            if (CountManyOfDistrbutionOneMainGroup >= 3)
                            {
                                GeneralMsgDto ErrorMsgs = new GeneralMsgDto(
                                                        IErrorMsgs.FAILED_GRATE_LEMET,
                                                        "Enter the Courect data",
                                                        "You exceeded the limit in distributing the main group over the periods, as the limit is distributing the group over 3 periods" + CountManyOfDistrbutionOneMainGroup
                                                        );
                                return ErrorMsgs;
                            }
                            if (ValedationDistrbutionOneMainGroupInTheDapatments != 0)
                            {
                                GeneralMsgDto ErrorMsgs = new GeneralMsgDto(
                                                        IErrorMsgs.FAILED_distributed_to_more_than_one_department,
                                                        "Enter the Courect  data ",
                                                        "The main group must be distributed to more than one department during its shift in 3 periods"
                                                        );
                                return ErrorMsgs;
                            }

                            else
                            {
                                try
                                {
                                    OldDistributionsMainGroupData.DepartmentId = NewDistributionsMainGroupDto.DepartmentId;
                                    _context.SaveChanges();
                                    GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                                    SuccessfullyMsgs.LECTURE_SCHEDULE_UPDATED,
                                                                    "Successfully Update",
                                                                    "You are Update this Distributions Main Group "
                                                                    );
                                    return SucMsg;
                                }
                                catch (Exception ex)
                                {
                                    GeneralMsgDto ErrorMsgs = new GeneralMsgDto(
                                                            IErrorMsgs.INVALID_DATA_FORMAT,
                                                            "Enter the Courect sympole of the Main Group",
                                                            "Error in data entry. The data entered is not included "
                                                            );
                                    return ErrorMsgs;
                                }

                            }    
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }

        }
        public List<ShowDistrbutionMainQDto> ShowDistributionFroTheMainGroup(int MainGroupId)
        {
            try
            {
                string sql = @"	 
                                 select 
                            	    d.DistributionsMainGroupId,
	                                m.MainGroupSympole,
	                                m.AcademicYearId,
	                                s.DepartmentName,
	                                r.RotationName,
	                                r.StartRotationDate,
	                                r.EndRotationDate
	                                from DistributionsMainGroup d 
	                                join MainGroup m on m.MainGroupId = d.MainGroupId
	                                join Department s on s.DepartmentId = d.DepartmentId
	                                join Rotations r on r.RotationId = d.RotationId

	                                where 
	                                m.MainGroupId= @MainGroupId
                            ";
                var result = _context.Database.SqlQueryRaw<ShowDistrbutionMainQDto>(
                    sql, new SqlParameter("MainGroupId", MainGroupId)).ToList();
                if (result == null)
                    return null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<ShowDistrbutionMainQDto> ShowDistributionMainGroupFroTheLevel(int Level)
        {
            try
            {
                string sql = @"	 
                        	    select
                            	    d.DistributionsMainGroupId,
	                                m.MainGroupSympole,
	                                a.AcademicYearId,
	                                s.DepartmentName,
	                                r.RotationName,
	                                r.StartRotationDate,
	                                r.EndRotationDate
	                                from DistributionsMainGroup d 
	                                join MainGroup m on m.MainGroupId = d.MainGroupId
	                                join Department s on s.DepartmentId = d.DepartmentId
	                                join Rotations r on r.RotationId = d.RotationId
	                                join AcademicYear a on m.AcademicYearId = a.AcademicYearId

	                                where 
                                    a.AcademicYearLevel = @Level
                            ";
                var result = _context.Database.SqlQueryRaw<ShowDistrbutionMainQDto>(
                    sql, new SqlParameter("Level", Level)).ToList();
                if (result == null)
                    return null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public List<GetDistrbutionMianGroupQ1Dto> ShowDistributionMainGroupByLevelAndRotationId(int Level ,string AcademicYearName)
        {
            try
            {
                string sql = @"	 
                        	    select 
                            	    d.DistributionsMainGroupId,
	                                m.MainGroupSympole,
                                    m.AcademicYearId,
	                                s.DepartmentName,
	                                r.RotationName,
	                                r.StartRotationDate,
	                                r.EndRotationDate
	                                from DistributionsMainGroup d 
	                                join MainGroup m on m.MainGroupId = d.MainGroupId
	                                join Department s on s.DepartmentId = d.DepartmentId
	                                join Rotations r on r.RotationId = d.RotationId

	                                where m.AcademicYearId = @Level
	                                and m.AcademicYearName = @AcademicYearName

                            ";
                var result = _context.Database.SqlQueryRaw<GetDistrbutionMianGroupQ1Dto>(
                    sql, new SqlParameter("Level", Level), new SqlParameter("AcademicYearName", AcademicYearName)).ToList();
                if (result == null)
                    return null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<ShowDistrbutionMainQDto> ShowDistributionFroTheMainGroupByDistributionsMainGroupId(int DistributionsMainGroupId)
        {
            try
            {
                string sql = @"	 
                                 select 
                            	    d.DistributionsMainGroupId,
	                                m.MainGroupSympole,
	                                m.AcademicYearId,
	                                s.DepartmentName,
	                                r.RotationName,
	                                r.StartRotationDate,
	                                r.EndRotationDate
	                                from DistributionsMainGroup d 
	                                join MainGroup m on m.MainGroupId = d.MainGroupId
	                                join Department s on s.DepartmentId = d.DepartmentId
	                                join Rotations r on r.RotationId = d.RotationId

	                                where 
                                    d.DistributionsMainGroupId =@DistributionsMainGroupId                       
                                ";
                var result = _context.Database.SqlQueryRaw<ShowDistrbutionMainQDto>(
                    sql, new SqlParameter("DistributionsMainGroupId", DistributionsMainGroupId)).ToList();
                if (result == null)
                    return null;
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

    }
}
