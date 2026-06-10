using DataBase.DBcon;
using DataBase.entitys;
using DevetionStudetns.DTO.AppointmentsDTO;
using DevetionStudetns.DTO.RotationsDTO;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Mappers.RotaionsMappers;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using loginpage.ErrorMsgs;

namespace DevetionStudetns.Repositry.RotationsRepositry
{
    public class RotationsRepo : IRotations
    {
        private readonly DBC _context;
        public RotationsRepo(DBC dBC)
        {
            _context = dBC;
        }
        public GeneralMsgDto AddRotations(AddRotaionsDto addRotaionsDto)
        {

            if (addRotaionsDto == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.MUST_FILL_ALL_FILLED,
                            "Enter the requird filled",
                            "there is not any data"
                            );
                return ErrorMsg;
            }
            else if (addRotaionsDto.StartRotationDate >= addRotaionsDto.EndRotationDate)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.END_DATE_MUST_GRATER,
                            "FAILED",
                            "Enter the Couret first Date And end Date . The Start Date must to be less than End Date "
                            );
                return ErrorMsg;
            }
            var ValedationDublecateDateStart = _context.Rotations.Where(p => p.StartRotationDate == addRotaionsDto.StartRotationDate).ToList().Count;
            var ValedationDublecateDateEnd = _context.Rotations.Where(p => p.EndRotationDate == addRotaionsDto.EndRotationDate).ToList().Count;
            if (ValedationDublecateDateStart != 0 || ValedationDublecateDateEnd != 0)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.FAILED_DATE,
                            "Error ",
                            "Error Dublecated Date in other rotation "
                            );
                return ErrorMsg;
            }
            var addRotation = _context.Rotations.Where(p => p.AcademicYearName == addRotaionsDto.AcademicYearName).ToList().Count;
            if (addRotation >= 3)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.MAXUMUM_SIZE_OF_ROTATION,
                            "Error ",
                            "You have exceeded the permissible limit "
                            );
                return ErrorMsg;
            }
            else
            {
                bool isOverlapping = _context.Rotations
                .Any(d =>
                    (addRotaionsDto.StartRotationDate >= d.StartRotationDate && addRotaionsDto.StartRotationDate <= d.EndRotationDate) ||
                    (addRotaionsDto.EndRotationDate >= d.StartRotationDate && addRotaionsDto.EndRotationDate <= d.EndRotationDate) ||
                    (d.StartRotationDate >= addRotaionsDto.StartRotationDate && d.StartRotationDate <= addRotaionsDto.EndRotationDate) ||
                    (d.EndRotationDate >= addRotaionsDto.StartRotationDate && d.EndRotationDate <= addRotaionsDto.EndRotationDate)
                );

                if (!isOverlapping)
                {
                    var GetDublecateNameOfRoation = _context.Rotations.Where(p => p.AcademicYearName == addRotaionsDto.AcademicYearName && p.RotationName == addRotaionsDto.RotationName).ToList().Count;
                    if (GetDublecateNameOfRoation == 0)
                    {

                        try
                        {
                            _context.Rotations.Add(addRotaionsDto.AddRotations());
                            _context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                  IErrorMsgs.INVALID_DATA_FORMAT,
                                                  "Enter the Courect sympole of the Rotations",
                                                  "Error in data entry. The data entered is not included "
                                                  );
                            return ErrorMsg;
                        }
                        GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                         SuccessfullyMsgs.WORKSHOP_REGISTERED_SUCCESSFULLY,
                                         "Add Successfully",
                                         "You Are Add New Rotations "
                                         );
                        return SuccessfullyMsg;
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.DUBLECATE_ROTATON_NAME,
                                              "DUBLECATE",
                                              "DUBLECATE "
                                              );
                        return ErrorMsg;
                    }
                }
                else
                {

                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                              IErrorMsgs.ERROR_ROTATION,
                                              "Enter the Courect sympole of the Rotations",
                                              "Error in data entry. The data entered is not included "
                                              );
                    return ErrorMsg;
                }


            }
        }
        public GeneralMsgDto DeleteRotations(int RotationsId)
        {
            if (RotationsId == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.INVALID_DATA_FORMAT,
                           "Enter the requird filled",
                           Convert.ToString(RotationsId)
                           );
                return ErrorMsg;
            }
            else
            {
                var GetRotationsIdForDelete = _context.Rotations.FirstOrDefault(p => p.RotationId == RotationsId);
                if (GetRotationsIdForDelete != null)
                {

                    _context.Rotations.Remove(GetRotationsIdForDelete);
                    _context.SaveChanges();
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                   SuccessfullyMsgs.SUCCESSFUL_DELETE,
                                                   "Successfully Delete",
                                                   "You are delete this Appointment "
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
        public GeneralMsgDto UpdateRotations(AddRotaionsDto NewRotaionsData, int RotaionstId)
        {
            if (RotaionstId == null || NewRotaionsData == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                       IErrorMsgs.INVALID_DATA_FORMAT,
                                                       "Enter the requird filled",
                                                       Convert.ToString(RotaionstId)
                                                       );
                return ErrorMsg;
            }
            else if (NewRotaionsData.StartRotationDate >= NewRotaionsData.EndRotationDate)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.END_DATE_MUST_GRATER,
                            "error ",
                            "error"
                            );
                return ErrorMsg;
            }
            else
            {
                var OldAppotmentData = _context.Rotations.FirstOrDefault(p => p.RotationId == RotaionstId);

                if (OldAppotmentData == null)
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
                    bool isOverlapping = _context.Rotations
                .Any(d =>
                    d.RotationId != RotaionstId && 
                    (
                        (NewRotaionsData.StartRotationDate >= d.StartRotationDate && NewRotaionsData.StartRotationDate <= d.EndRotationDate) ||
                        (NewRotaionsData.EndRotationDate >= d.StartRotationDate && NewRotaionsData.EndRotationDate <= d.EndRotationDate) ||
                        (d.StartRotationDate >= NewRotaionsData.StartRotationDate && d.StartRotationDate <= NewRotaionsData.EndRotationDate) ||
                        (d.EndRotationDate >= NewRotaionsData.StartRotationDate && d.EndRotationDate <= NewRotaionsData.EndRotationDate)
                    ));
                    
                    if (!isOverlapping)
                    {
                        var ValidationNameOfTheRotaion = _context.Rotations.Where(p => p.RotationName == NewRotaionsData.RotationName && p.RotationId != RotaionstId).ToList().Count; 
                        var getDublecatedDate = _context.Rotations.Where(p => p.RotationName == NewRotaionsData.RotationName && p.EndRotationDate == NewRotaionsData.EndRotationDate 
                        && p.StartRotationDate == NewRotaionsData.StartRotationDate && p.AcademicYearName == NewRotaionsData.AcademicYearName).ToList().Count;
                        if (ValidationNameOfTheRotaion == 0)
                        {
                            DateOnly newStart = NewRotaionsData.StartRotationDate;
                            DateOnly newEnd = NewRotaionsData.EndRotationDate;
                            bool hasConflict = _context.Appointment
                                .Any(a => a.RotationId == RotaionstId &&
                                 a.StartSessionDate < newStart || a.EndSessionDate > newEnd);
                            if (!hasConflict)
                            {
                                if (getDublecatedDate == 0 ) 
                                {
                                    try
                                    {
                                        OldAppotmentData.StartRotationDate = NewRotaionsData.StartRotationDate;
                                        OldAppotmentData.EndRotationDate = NewRotaionsData.EndRotationDate;
                                        OldAppotmentData.RotationName = NewRotaionsData.RotationName;
                                        _context.SaveChanges();

                                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                       SuccessfullyMsgs.ROTATION_SCHEDULE_UPDATED,
                                                                       "Successfully Update",
                                                                       "You are Update this Rotations "
                                                                       );
                                        return ErrorMsg;
                                    }
                                    catch (Exception ex)
                                    {

                                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                       IErrorMsgs.Error,
                                                                       "Enter the requird filled",
                                                                       ex.InnerException?.Message
                                                                       );
                                        return ErrorMsg;
                                    }
                                }
                                else
                                {
                                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                       IErrorMsgs.CONFLICT_DATE_ROTATION,
                                                                       "Enter the requird filled",
                                                                       "Error"
                                                                       );
                                    return ErrorMsg;
                                }
                            }
                            else
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                            IErrorMsgs.OUT_OF_LEMEIT,
                                                                            "DUPLICATE_RECORD_RERRO",
                                                                            "DUPLICATE_RECORD_RERRO"
                                                                            );
                                return ErrorMsg;
                            }
                        }
                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                                              IErrorMsgs.DUBLICATE_ROTATION_NAME,
                                                                              "Dublicate Rotation Name ",
                                                                              "Dublicate Rotation Name "
                                                                              );
                            return ErrorMsg;
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                  IErrorMsgs.ERROR_ROTATION,
                                                  "Enter the Courect sympole of the Rotations",
                                                  "Error in data entry. The data entered is not included "
                                                  );
                        return ErrorMsg;
                    }
                }
            }
        }

        public List<GetRotaionDto> ShowDateRotaionInOneYear(string AcadimicYear)
        {
            List<Rotation> rotations = _context.Rotations.Where(p => p.AcademicYearName == AcadimicYear).ToList();
            List<GetRotaionDto> ShowRotaion = new List<GetRotaionDto>();
            if (rotations != null)
            {
                foreach (Rotation rotation in rotations)
                {
                    ShowRotaion.Add(rotation.ShowRotations());
                }
                return ShowRotaion;
            }
            else
            {
                return null;
            }
        }
        public GetRotaionDto ShowDateRotaionByRotationId(int RotationId)
        {
            Rotation rotations = _context.Rotations.FirstOrDefault(p => p.RotationId == RotationId);
            GetRotaionDto ShowRotaion = rotations.ShowRotations();
            if (ShowRotaion != null)
            {

                return ShowRotaion;
            }
            else
            {
                return null;
            }
        }
    }
}
