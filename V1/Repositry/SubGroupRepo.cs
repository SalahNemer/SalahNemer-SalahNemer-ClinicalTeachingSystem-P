using DataBase.DBcon;
using DevetionStudetns.DTO.AppointmentsDTO;
using DevetionStudetns.DTO.DistributionsMainGroupDTO;
using DevetionStudetns.DTO.MainGroupDTO;
using DevetionStudetns.DTO.SubGroupDTO;
using DevetionStudetns.Entity;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Mappers.SubGroupMapper;
using DevetionStudetns.NewFolder;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DevetionStudetns.Repositry.SubGroupRepositry
{
    public class SubGroupRepo: ISubGroup
    {
        private readonly DBC _context;
        public SubGroupRepo (DBC context)
        {
            _context = context;
        }
        
        public GeneralMsgDto AddSubGroup (AddSubGroupDto supGroupDto)
        {


            if (supGroupDto == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.MUST_FILL_ALL_FILLED,
                            "Enter the requird filled",
                            "there is not any data"
                            );
                return ErrorMsg;
            }
            var lemetSubGroupInOneMainGroup = _context.subGroups.Where(p => p.MainGroupId == supGroupDto.MainGroupId).ToList().Count; 
            if (lemetSubGroupInOneMainGroup > 8)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                            IErrorMsgs.MAXUMUM_SIZE_OF_GROUP,
                                            "Enter the requird filled",
                                            "The main Group limite 9 sub Group only "
                                            );
                return ErrorMsg;
            }
            else
            {
            var valedationSympoleSubGroup  = _context.subGroups.FirstOrDefault(p => p.MainGroupId == supGroupDto.MainGroupId && p.SubGroupSympole == supGroupDto.SubGroupSympole);
            var valedationMainGroupIsValed  = _context.mainGrops.Where(p=>p.MainGroupId ==  supGroupDto.MainGroupId ).ToList().Count;
                if (valedationMainGroupIsValed == 0)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                         IErrorMsgs.NOT_FOUND_MAINGROUP,
                                         "Enter the Courect id of the Main Group",
                                         "There is not any main Group have this id : "+ supGroupDto.MainGroupId
                                         );
                    return ErrorMsg;
                }
                if (valedationSympoleSubGroup != null )
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                         IErrorMsgs.DUPLICATE_RECORD_RERRO,
                                         "Enter the Courect sympole of the Main Group",
                                         "Error in data entry. The data entered is duplcate"
                                         );
                    return ErrorMsg;
                }
                 
                var GetMainGroupSymble = _context.subGroups.Where(p => p.MainGrop.MainGroupSympole == supGroupDto.SubGroupSympole).ToList().Count;
                if (GetMainGroupSymble != 0 )
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                         IErrorMsgs.subgroup_code_must_not_match_the_main_group,
                                         "Fialed",
                                         "Error the main Group symble smae the sub Group symple "
                                         );
                    return ErrorMsg;
                }
                else
                {


                    try
                    {
                        _context.subGroups.Add(supGroupDto.AddSupGroupMapper());
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
                                     "You Are Add subGroup "
                                     );
                    return SuccessfullyMsg;
                }
            }

           
        }
        public GeneralMsgDto DeleteSubGroup(int subGrooupId)
        {
            if (subGrooupId == null)
            {
                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.INVALID_DATA_FORMAT,
                           "Enter the requird filled",
                           Convert.ToString(subGrooupId)
                           );
                return ErrorMsg;
            }
            else
            {
                var GetSubGroupDeleted = _context.subGroups.FirstOrDefault(p => p.SubGroupId == subGrooupId);
                if (GetSubGroupDeleted != null)
                {
                    _context.subGroups.Remove(GetSubGroupDeleted);
                    _context.SaveChanges();
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                                   SuccessfullyMsgs.SUCCESSFUL_DELETE,
                                                   "Successfully Delete",
                                                   "You are delete this Distributions Main Group "
                                                   );
                    return ErrorMsg;
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                           IErrorMsgs.NOT_FOUND_SUBGROUP_BYSEARCH,
                           "Enter the requird filled",
                           "Ther is not any data to delete it "
                           );
                    return ErrorMsg;
                }
            }
           
        }
        public List<GetSubGroupDto> GetSubGroup(int MainGroupId)
        {
            List<SubGroup> GetSub_Group = _context.subGroups.Where(p => p.MainGroupId == MainGroupId).ToList();
            if (GetSub_Group == null)
            {
                return null; 
            }
            else
            {
                List<GetSubGroupDto> getSubGeoupByDto = new List<GetSubGroupDto>(); 
                foreach(SubGroup subGroup in GetSub_Group)
                {
                    getSubGeoupByDto.Add(subGroup.showSubGroupMapper());
                }
                return getSubGeoupByDto; 
            }
        }

        public List<GetSubGroupDto> GetAllSuBGroupByMainGroupIdRepo(int MainGroupId)
        {
            List<SubGroup> GetAllsubGroupsInOnMainGroupById = _context.subGroups.Where(p => p.MainGroupId.Equals(MainGroupId)).ToList();
            if (GetAllsubGroupsInOnMainGroupById == null)
                return null;
            List<GetSubGroupDto> showSupGroupDtos = new List<GetSubGroupDto>();
            foreach (SubGroup sub in GetAllsubGroupsInOnMainGroupById)
            {
                showSupGroupDtos.Add(sub.showSubGroupMapper());
            }
            return showSupGroupDtos;
        }

        public GetSubGroupDto GetSubGroupById(int SubGroupId)
        {
            var GetSubGroupBySubGroupId = _context.subGroups.FirstOrDefault(p=>p.SubGroupId.Equals(SubGroupId));
            if (GetSubGroupBySubGroupId == null)
                return null;
            GetSubGroupDto showSupGroupDto = GetSubGroupBySubGroupId.showSubGroupMapper();
            return showSupGroupDto; 
        }

        public GeneralMsgDto UpdateSubGroupById(AddSubGroupDto NewDataSubGoup, int subGroupId)
        {
            if (subGroupId == null || NewDataSubGoup == null)
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
                var OldDataSubGroup = _context.subGroups.FirstOrDefault(p => p.SubGroupId == subGroupId);
                if ( OldDataSubGroup == null)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                         IErrorMsgs.NOT_FOUND_MAINGROUP,
                                         "Enter the Courect id of the Main Group",
                                         "There is not any main Group have this id : " + NewDataSubGoup.MainGroupId
                                         );
                    return ErrorMsg;
                }
                if (NewDataSubGoup.MainGroupId == OldDataSubGroup.MainGroupId && NewDataSubGoup.SubGroupSympole == OldDataSubGroup.SubGroupSympole)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                             IErrorMsgs.DUPLICATE_RECORD_RERRO,
                                             "Enter the Courect sympole of the Main Group",
                                             "Error in data entry. The data entered is duplcate"
                                             );
                    return ErrorMsg;
                }
                var GetMainGroupSymble = _context.mainGrops.FirstOrDefault(p => p.MainGroupId == NewDataSubGoup.MainGroupId);
                if (GetMainGroupSymble.MainGroupSympole == NewDataSubGoup.SubGroupSympole)
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                         IErrorMsgs.subgroup_code_must_not_match_the_main_group,
                                         "Fialed",
                                         "Error the main Group symble smae the sub Group symple "
                                         );
                    return ErrorMsg;
                }
                else
                {
                    var valedationSympoleSubGroup = _context.subGroups.Where(p => p.MainGroupId == NewDataSubGoup.MainGroupId && p.SubGroupSympole == NewDataSubGoup.SubGroupSympole).ToList().Count;         
                    var valedationMainGroupIsValed = _context.mainGrops.Where(p => p.MainGroupId == NewDataSubGoup.MainGroupId).ToList();
                    if (valedationMainGroupIsValed == null)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                             IErrorMsgs.INVALID_DATA_FORMAT,
                                             "Enter the Courect sympole of the Main Group",
                                             "There is not any main Group have this id : " + NewDataSubGoup.MainGroupId
                                             );
                        return ErrorMsg;
                    }
                    if (valedationSympoleSubGroup >= 1)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                             IErrorMsgs.DUPLICATE_RECORD_RERRO,
                                             "Enter the Courect sympole of the Main Group",
                                             "Error in data entry. The data entered is duplcate" 
                                             );
                        return ErrorMsg;
                    }
                    var lemetSubGroupInOneMainGroup = _context.subGroups.Where(p => p.MainGroupId == NewDataSubGoup.MainGroupId).ToList().Count;

                    if ( NewDataSubGoup.MainGroupId == OldDataSubGroup.MainGroupId)
                    {
                        OldDataSubGroup.SubGroupSympole = NewDataSubGoup.SubGroupSympole;
                        OldDataSubGroup.MainGroupId = NewDataSubGoup.MainGroupId;

                        _context.SaveChanges();

                        GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                     SuccessfullyMsgs.UPDATED_DATA_SUCCESSFULLY,
                                     "Add Successfully",
                                     "You Are Update data  "
                                     );
                        return SuccessfullyMsg;
                    }
                    if (lemetSubGroupInOneMainGroup > 8)
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                            IErrorMsgs.MAXUMUM_SIZE_OF_GROUP,
                                            "Enter the requird filled",
                                            "The main Group limite 9 sub Group only "
                                            );
                        return ErrorMsg;
                    }
                    else
                    {
                        OldDataSubGroup.SubGroupSympole = NewDataSubGoup.SubGroupSympole;
                        OldDataSubGroup.MainGroupId = NewDataSubGoup.MainGroupId;

                        _context.SaveChanges();

                        GeneralMsgDto SuccessfullyMsg = new GeneralMsgDto(
                                     SuccessfullyMsgs.UPDATED_DATA_SUCCESSFULLY,
                                     "Add Successfully",
                                     "You Are Update data  "
                                     );
                        return SuccessfullyMsg;
                    }
                }

            }
        }
    }
}
