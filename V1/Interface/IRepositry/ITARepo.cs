using DataBase.Entity;
using loginpage.DBcon;
using V1.DTO.TADTO;

namespace DevetionStudetns.Interface
{
    public interface ITARepo
    {
        Task<IEnumerable<GetTAInformationQDto>> GetAllTAs();
        Task<TA?> GetTAById(string id);
        Task<GeneralMsgDto> AddTA(TA t);
        Task<GeneralMsgDto> UpdateTA(TA t);
        Task<GeneralMsgDto> DeleteTA(string id);
        public Task<GeneralMsgDto> UpdateTaAndUser(UpdateTaAndUserDto ta, string TaId);

    }
}
