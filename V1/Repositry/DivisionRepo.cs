using DataBase.DBcon;
using DataBase.entitys;
using DevetionStudetns.DTO.DivisionsDTO;
using DevetionStudetns.DTO.MainGroupDTO;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Mappers.DivisionMapper;
using DevetionStudetns.NewFolder;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using loginpage.ErrorMsgs;

namespace DevetionStudetns.Repositry.DivisionRepostry
{
    public class DivisionRepo : IDivision
    {
        private readonly DBC _context;
        public DivisionRepo( DBC dBC)
        {
            _context = dBC;
        }
        public GeneralMsgDto AddDivision (DivisionAddDto AddDivision)
        {
            try
            {
                if (AddDivision == null)
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
                    var SubGroupId = AddDivision.SubGroupId;
                    var StudentsId = AddDivision.StudentId;
                    var SubDivisionId = _context.Divisions.Where(d => d.SubGroupId == SubGroupId).ToList().Count + 1;
                    var ReplayDivisionInSameData = _context.Divisions.Where(d => d.SubGroupId == SubGroupId && d.StudentId == StudentsId).ToList().Count;
                    if (ReplayDivisionInSameData == 0)
                    {
                        var getNumberOfStudentsInOneSubGroup = _context.Divisions.Where(p => p.SubGroupId == SubGroupId).ToList().Count;
                        var AddDataInSubGroup = _context.subGroups.FirstOrDefault(p => p.SubGroupId == SubGroupId);
                        var ValedationGetsubGroupById = _context.subGroups.Where(p => p.SubGroupId == SubGroupId).ToList().Count;
                        var ValedationGetsStudentsById = _context.students.Where(p => p.UserId == StudentsId).ToList().Count;
                        int GetSumOfValedation = ValedationGetsubGroupById + ValedationGetsStudentsById;
                        var currentCount = _context.Divisions.Count(p => p.SubGroupId == SubGroupId);

                        if (currentCount <= 8)
                        {
                            if (GetSumOfValedation == 0)
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                          IErrorMsgs.STUDENTS_NOT_FOUND + " , " + IErrorMsgs.NOT_FOUND_SUBGROUP_BYSEARCH,
                                          "Not Fround ",
                                          "There is not any students have tihs id :  " + StudentsId + "  , and thers is not any subGroup have this id : " + SubGroupId
                                          );
                                return ErrorMsg;
                            }
                            if (ValedationGetsubGroupById == 0)
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                          IErrorMsgs.NOT_FOUND_SUBGROUP_BYSEARCH,
                                          "Not Fround ",
                                          "Thers is not any subGroup have this id : " + SubGroupId
                                          );
                                return ErrorMsg;
                            }
                            if (ValedationGetsStudentsById == 0)
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                          IErrorMsgs.STUDENTS_NOT_FOUND,
                                          "Not Fround ",
                                          "There is not any students have tihs id :  " + StudentsId
                                          );
                                return ErrorMsg;
                            }
                            else
                            {
                                try
                                {
                                    AddDataInSubGroup.NumberOfStudetns = getNumberOfStudentsInOneSubGroup;
                                    _context.Divisions.Add(AddDivision.AddDivisionMappers());
                                    _context.SaveChanges();
                                    GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                          SuccessfullyMsgs.STUDENT_REGISTERED_SUCCESSFULLY ,
                                          "Add Successfully",
                                          "You Are Add New Division " 
                                          );
                                    return SuccessfullyMsg;
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
                            }
                        }
                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                               IErrorMsgs.MAXUMUM_SIZE_OF_GROUP,
                               "Enter the Courect sympole of the Main Group",
                               "Max number Of Students in the SubGroup : " + SubDivisionId + "  number of Studnts in this SubGroup"
                                 );
                            return ErrorMsg;
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                            IErrorMsgs.DUPLICATE_RECORD_RERRO,
                                            "Entering duplicate data",
                                            "Enter Data with no ant duplicate "
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

        public GeneralMsgDto DeleteDivision(int DivisionId)
        {
            try
            {
                if (DivisionId == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                               IErrorMsgs.INVALID_DATA_FORMAT,
                    "Enter the requird filled",
                               Convert.ToString(DivisionId)
                               );
                    return ErrorMsg;
                }
                else
                {
                    var DeleteDeivision = _context.Divisions.FirstOrDefault(p => p.DivisionId == DivisionId);
                    if (DeleteDeivision != null)
                    {
                        _context.Divisions.Remove(DeleteDeivision);
                        _context.SaveChanges();
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                       SuccessfullyMsgs.SUCCESSFUL_DELETE,
                                                       "Successfully Delete",
                                                       "You are delete this Division  "
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
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }

        }
        public GeneralMsgDto UpdateDivision (DivisionAddDto NewData , int DivisionId)
        {
            try
            {
                if (DivisionId == 0)
                {
                    return null;
                }
                var SubGroupId = NewData.SubGroupId;
                var StudentsId = NewData.StudentId;
                var SubDivisionId = _context.Divisions.Where(d => d.SubGroupId == SubGroupId).ToList().Count;
                var ReplayDivisionInSameData = _context.Divisions.Where(d => d.SubGroupId == SubGroupId && d.StudentId == StudentsId).ToList().Count;
                var OldData = _context.Divisions.Find(DivisionId);
                if (ReplayDivisionInSameData == 0)
                {
                    var getNumberOfStudentsInOneSubGroup = _context.Divisions.Where(p => p.SubGroupId == SubGroupId).ToList().Count;
                    var AddDataInSubGroup = _context.subGroups.FirstOrDefault(p => p.SubGroupId == SubGroupId);
                    var ValedationGetsubGroupById = _context.subGroups.Where(p => p.SubGroupId == SubGroupId).ToList().Count;
                    var ValedationGetsStudentsById = _context.students.Where(p => p.UserId == StudentsId).ToList().Count;
                    int GetSumOfValedation = ValedationGetsubGroupById + ValedationGetsStudentsById;

                    if (7 >= SubDivisionId)
                    {
                        if (GetSumOfValedation == 0)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                      IErrorMsgs.STUDENTS_NOT_FOUND + " , " + IErrorMsgs.NOT_FOUND_SUBGROUP_BYSEARCH,
                                      "Not Fround ",
                                      "There is not any students have tihs id :  " + StudentsId + "  , and thers is not any subGroup have this id : " + SubGroupId
                                      );
                            return ErrorMsg;
                        }
                        if (ValedationGetsubGroupById == 0)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                      IErrorMsgs.NOT_FOUND_SUBGROUP_BYSEARCH,
                                      "Not Fround ",
                                      "Thers is not any subGroup have this id : " + SubGroupId
                                      );
                            return ErrorMsg;
                        }
                        if (ValedationGetsStudentsById == 0)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                      IErrorMsgs.STUDENTS_NOT_FOUND,
                                      "Not Fround ",
                                      "There is not any students have tihs id :  " + StudentsId
                                      );
                            return ErrorMsg;
                        }
                        else
                        {
                            try
                            {
                                AddDataInSubGroup.NumberOfStudetns = getNumberOfStudentsInOneSubGroup;
                                OldData.StudentId = NewData.StudentId;
                                OldData.SubGroupId = NewData.SubGroupId;
                                _context.SaveChanges();
                                GetDivisionDto showDivisionMap = OldData.showDivisionMap();
                                GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                      SuccessfullyMsgs.UPDATE_SUCCESSFUL,
                                      "Add Successfully",
                                      "You Are Add New Division "
                                      );
                                return SuccessfullyMsg;
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
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.MAXUMUM_SIZE_OF_GROUP,
                           "Enter the Courect sympole of the Main Group",
                           "Max number Of Students in the SubGroup : " + SubDivisionId + "  number of Studnts in this SubGroup"
                             );
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.DUPLICATE_RECORD_RERRO,
                                        "Entering duplicate data",
                                        "Enter Data with no ant duplicate "
                                        );
                    return ErrorMsg;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetDivisionDto> GetAllStudentsInOneSubGroupBySubGroupId(int SubGroupId)
        {
            try
            {
                List<Division> getAllStudetnsInSubGroup = _context.Divisions.Where(p=>p.SubGroupId == SubGroupId).ToList();
                List<GetDivisionDto> divisionDtos = new List<GetDivisionDto>();
                if (getAllStudetnsInSubGroup == null)
                {
                    return null;
                }
                foreach(Division divisions in getAllStudetnsInSubGroup)
                {
                    divisionDtos.Add(divisions.showDivisionMap());
                }
                return divisionDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        } 
        public List<GetDivisionDto> GetAllStudentsInOneMainByMainGroupId(int MainGroupId)
        {
            try
            {
                List<Division> getAllStudetnsInMainGroup = _context.Divisions.Where(p => p.SubGroup.MainGroupId == MainGroupId).ToList();
                List<GetDivisionDto> divisionDtos = new List<GetDivisionDto>();
                if (getAllStudetnsInMainGroup == null)
                {
                    return null;
                }
                foreach (Division divisions in getAllStudetnsInMainGroup)
                {
                    divisionDtos.Add(divisions.showDivisionMap());
                }
                return divisionDtos;

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetDivisionDto> GetAllStudentsInTheSameLevel(int Level)
        {
            try
            {
                List<Division> getAllStudetnsInTheSameLevel = _context.Divisions.Where(p => p.SubGroup.MainGrop.AcademicYearId == Level).ToList();
                List<GetDivisionDto> divisionDtos = new List<GetDivisionDto>();
                if (getAllStudetnsInTheSameLevel == null)
                {
                    return null;
                }
                foreach (Division divisions in getAllStudetnsInTheSameLevel)
                {
                    divisionDtos.Add(divisions.showDivisionMap());
                }
                return divisionDtos;

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
        public List<GetDivisionDto> GetDataForStudentId(string StudentId)
        {
            try
            {
                List<Division> getInfomaionAboutStudentsId = _context.Divisions.Where(p => p.StudentId == StudentId).ToList();
                List<GetDivisionDto> divisionDtos = new List<GetDivisionDto>();
                if (getInfomaionAboutStudentsId == null)
                {
                    return null;
                }
                foreach (Division divisions in getInfomaionAboutStudentsId)
                {
                    divisionDtos.Add(divisions.showDivisionMap());
                }
                return divisionDtos;

            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

    }
}
