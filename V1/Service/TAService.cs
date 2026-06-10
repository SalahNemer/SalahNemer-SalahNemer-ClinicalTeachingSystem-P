using DataBase.Entity;
using DevetionStudetns.DTO.TADTO;
using DevetionStudetns.Interface;
using DevetionStudetns.Mappers.TAMapper;
using loginpage.DBcon;
using V1.DTO.TADTO;

namespace DevetionStudetns.Service
{
    public class TAService : ITAService
    {
        private readonly ITARepo _tARepo;
        public TAService(ITARepo tARepo)
        {
            _tARepo = tARepo;
        }
        public async Task<IEnumerable<GetTAInformationQDto>> GetAllTAs()
        {
            return await _tARepo.GetAllTAs();
        }
        public async Task<AddTADto> GetTAById(string id)
        {
            var ta = await _tARepo.GetTAById(id);
            return  ta != null ? TAMapper.ShowTA(ta) : null;
        }
        public async Task<GeneralMsgDto> AddTA(AddTADto tADTO)
        {
            return await _tARepo.AddTA(TAMapper.AddOrEditTA(tADTO));           
        }
        public async Task<GeneralMsgDto> UpdateTA(string id, UpdateTaDto tADTO)
        {
            var ta = new TA
            {
                TAId = id,
                SupervisedYear = tADTO.SupervisedYear,
            };
            return await _tARepo.UpdateTA(ta);         
        }
        public async Task<GeneralMsgDto> DeleteTA(string id)
        {
            return await _tARepo.DeleteTA(id);
        }

        public async Task<GeneralMsgDto> UpdateTaAndUser(UpdateTaAndUserDto ta, string TaId)
        {
            return await _tARepo.UpdateTaAndUser(ta, TaId);
        }


    }

}
