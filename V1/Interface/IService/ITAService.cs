using DevetionStudetns.DTO.TADTO;
using loginpage.DBcon;
using V1.DTO.TADTO;

namespace DevetionStudetns.Interface
{
    public interface ITAService
    {
        Task<IEnumerable<GetTAInformationQDto>> GetAllTAs();
        Task<AddTADto> GetTAById(string id);
        Task<GeneralMsgDto> AddTA(AddTADto tADTO);
        Task<GeneralMsgDto> UpdateTA(string id, UpdateTaDto tADTO);
        Task<GeneralMsgDto> DeleteTA(string id);
        public Task<GeneralMsgDto> UpdateTaAndUser(UpdateTaAndUserDto ta, string TaId);

    }
}
