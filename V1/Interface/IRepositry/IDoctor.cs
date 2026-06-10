using database.models;
using DevetionStudetns.DTO.DoctorDto;
using FinalProject.DTO.DoctorDto;
using loginpage.DBcon;
using V1.DTO.DoctorDTO;

namespace DevetionStudetns.Interface
{
    public interface IDoctor
    {
        public Task<List<DoctorDto>> Get();
        public Task<List<GetAllDataQDto>> Get_All_Data();
        public Task<List<GetAllDataQDto>> GetDoctorsByDepartement(int departementID);
        public Task<GeneralMsgDto> Add(DoctorDto doctors);
        public Task<GeneralMsgDto> Delete(string id);
        public Task<GeneralMsgDto> Update(string id, UpdateDoctorDto doctors);
        public List<GetDoctorQ1Dto> GetDoctorById(string DcotroId);
        public Task<GeneralMsgDto> UpdateDoctorAndUser(UpdateDoctorAndUserDto Doctor, string DoctorId);

    }
}
