using DataBase.DBcon;
using DevetionStudetns.Error.SuccessfullyMsg;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using V1.DTO.PolicieDTO;
using V1.Interface.IRepositry;
using V1.Mappers.PolicieMapper;

namespace V1.Repositry
{
    public class PolicieRepo : IPolicie
    {
        private readonly DBC _context;
        public PolicieRepo (DBC con)
        {
            _context = con;
        }

        public GeneralMsgDto AddPolicie(AddPolicieDto AddPolicie)
        {
            if (AddPolicie == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                            IErrorMsgs.NULL_DATA_FOR_POLICIE,
                                            "NULL DATA ",
                                            "NULL DATA"
                                            );
                return ErrorMsg;
            }
            else
            {
                var getCreaterId  = _context.Users.FirstOrDefault(p=>p.UserId == AddPolicie.CreatorId);
                if (getCreaterId == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                            IErrorMsgs.USER_NOT_FOUND,
                                            "Not found user id have same this id  "+ AddPolicie.CreatorId,
                                            "not found "
                                            );
                    return ErrorMsg;
                }
                else
                {
                    try
                    {
                        _context.Policies.Add(AddPolicie.AddPolicie());
                        _context.SaveChanges();
                        GeneralMsgDto SuccessMsg = new GeneralMsgDto(
                                            SuccessfullyMsgs.SUCCESSFULLY_APP_POLICIE,
                                            " Successfully Add Policie",
                                            " Successfully Add Policie "
                                            );
                        return SuccessMsg;

                    }
                    catch (Exception ex) {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                            IErrorMsgs.Error,
                                            "Error  " ,
                                            "Error "
                                            );
                        return ErrorMsg;
                    }
                }
            }
        }
        public GeneralMsgDto UpdatePolicie (UpdatePolicieDto newDataPolicieDto , int PolicieId)
        {
            if (newDataPolicieDto == null && PolicieId ==null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                            IErrorMsgs.NULL_DATA_FOR_POLICIE,
                                            "NULL DATA ",
                                            "NULL DATA"
                                            );
                return ErrorMsg;
            }
            else
            {
                var getPolicerById  = _context.Policies.FirstOrDefault(p=>p.PolicieId == PolicieId);
                if (getPolicerById == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                            IErrorMsgs.NOT_FOUBD_POLICIE,
                                            "Not Found",
                                            "Not Found "
                                            );
                    return ErrorMsg;
                }
                else
                {
                    try
                    {
                        getPolicerById.Title = newDataPolicieDto.Title;
                        getPolicerById.PolicyIdentifier = newDataPolicieDto.PolicyIdentifier;
                        getPolicerById.Objectives = newDataPolicieDto.Objectives;
                        getPolicerById.ExecutionResponsible = newDataPolicieDto.ExecutionResponsible;
                        getPolicerById.Procedures = newDataPolicieDto.Procedures;
                        getPolicerById.Forms = newDataPolicieDto.Forms;

                        _context.SaveChanges();
                        GeneralMsgDto SuccessMsg = new GeneralMsgDto(
                                            SuccessfullyMsgs.SUCCESSFULLY_UPDATE_POLICIE,
                                            " Successfully Update Policie",
                                            " Successfully Update Policie "
                                            );
                        return SuccessMsg;

                    }
                    catch (Exception ex)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                           IErrorMsgs.Error,
                                           "Error  ",
                                           "Error "
                                           );
                        return ErrorMsg;
                    }
                }
            }
        }
        public GeneralMsgDto DeletePolicie (int policieId)
        {
            if (policieId == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                            IErrorMsgs.NULL_DATA_FOR_POLICIE,
                                            "NULL DATA ",
                                            "NULL DATA"
                                            );
                return ErrorMsg;
            }
            else
            {
                var getPoliceById = _context.Policies.FirstOrDefault(p => p.PolicieId == policieId);
                if (getPoliceById != null)
                {
                    try
                    {
                        _context.Policies.Remove(getPoliceById);
                        _context.SaveChanges();
                        GeneralMsgDto SuccessMsg = new GeneralMsgDto(
                                            SuccessfullyMsgs.SUCCESSFULLY_DELETE_POLICIE,
                                            " Successfully Delete Policie",
                                            " Successfully Delete Policie "
                                            );
                        return SuccessMsg;
                    }
                    catch (Exception ex)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                           IErrorMsgs.Error,
                                           "Error  ",
                                           "Error "
                                           );
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                            IErrorMsgs.NOT_FOUBD_POLICIE,
                                            "Not Found",
                                            "Not Found "
                                            );
                    return ErrorMsg;
                }
            }
        }
        public List<GetPolicieQDto> GetAllPolicie()
        {
            string sql = @"
                        select 
                        a.PolicieId,
                        a.Title,
                        a.PolicyIdentifier,
                        a.Objectives,
                        a.ExecutionResponsible,
                        a.Procedures,
                        a.Forms
                        from Policie a
                        ";
            var Result = _context.Database.SqlQueryRaw<GetPolicieQDto>(sql).ToList();
            if (Result == null)
            {
                return null;
            }
            else
            {
                return Result;
            }
        }
        public List<GetPolicieQDto> GetAllPolicieById(int PolicieId )
        {
            string sql = @"
                        select 
                        a.PolicieId,
                        a.Title,
                        a.PolicyIdentifier,
                        a.Objectives,
                        a.ExecutionResponsible,
                        a.Procedures,
                        a.Forms
                        from Policie a
                        where a.PolicieId = @PolicieId
                        ";
            var Result = _context.Database.SqlQueryRaw<GetPolicieQDto>(sql , 
                new SqlParameter ("PolicieId", PolicieId)).ToList();
            if (Result == null)
            {
                return null;
            }
            else
            {
                return Result;
            }
        }
    }
}
