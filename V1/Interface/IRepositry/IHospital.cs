using database.models;
using DevetionStudetns.DTO.Hospital;
using loginpage.DBcon;

namespace DevetionStudetns.Interface
{
    public interface IHospital
    {
        public Task<List<GetHospitalDto>> GetHospital();  
        public Task<GeneralMsgDto> AddHospital(HospitalDto hospital);
        public Task<GeneralMsgDto> UpdateHospitalData(int id,HospitalDto dto);
        public Task<GeneralMsgDto> DeleteHospital(int id);
        public  Task<List<GetHospitalDto>> GetHospitalById(int hospitalId);
    }
}
